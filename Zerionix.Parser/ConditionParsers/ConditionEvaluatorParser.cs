using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;
using Zerionix.Domain.ParserCommands;
using Zerionix.Parser.Interfaces.ConditionParserInterfaces;

namespace Zerionix.Parser.ConditionParsers
{

    public class ConditionEvaluatorParser : IConditionEvaluatorParser
    {
        private readonly IEnumerable<IBinaryOperationEvaluationParser> _binaryOperationEvaluationParsers;

        public ConditionEvaluatorParser(IEnumerable<IBinaryOperationEvaluationParser> binaryOperationEvaluationParsers)
        {
            _binaryOperationEvaluationParsers = binaryOperationEvaluationParsers;
        }

        public EvaluationResult Evaluate(IOperation operation, MethodGlobalStore methodGlobalStore)
        {
            var binaryOperationEvaluateContext = new BinaryOperationEvaluateContext
            {
                Operation = operation,
                MethodGlobalStore = methodGlobalStore
            };

            if (operation is IBinaryOperation binary)
            {
                binaryOperationEvaluateContext.LeftEvaluationResult = Evaluate(binary.LeftOperand, methodGlobalStore);
                binaryOperationEvaluateContext.RightEvaluationResult = Evaluate(binary.RightOperand, methodGlobalStore);
            }

            if (operation is IUnaryOperation unary && unary.OperatorKind == UnaryOperatorKind.Not)
            {
                return Evaluate(unary.Operand, methodGlobalStore) switch
                {
                    EvaluationResult.True => EvaluationResult.False,
                    EvaluationResult.False => EvaluationResult.True,
                    _ => EvaluationResult.Unknown
                };
            }

            foreach(var parser in _binaryOperationEvaluationParsers.Where(p => p.CanHandle(operation)))
            {
                var result = parser.EvaluateBinaryOperation(binaryOperationEvaluateContext);

                if (result is EvaluationResult evalResult)
                    return evalResult;
            }

            return EvaluationResult.Unknown;
        }
    }
}
