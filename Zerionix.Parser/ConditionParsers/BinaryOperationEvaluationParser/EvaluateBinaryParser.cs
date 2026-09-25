using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;
using Zerionix.Domain.ParserCommands;
using Zerionix.Parser.Interfaces.ConditionParserInterfaces;

namespace Zerionix.Parser.ConditionParsers.BinaryOperationEvaluationParser
{
    public class EvaluateBinaryParser : IBinaryOperationEvaluationParser
    {
        private readonly ICompareValueParser _compareValueParser;
        private readonly IEvaluateValueParser _evaluateValueParser;

        public EvaluateBinaryParser(ICompareValueParser compareValueParser, IEvaluateValueParser evaluateValueParser)
        {
            _compareValueParser = compareValueParser;
            _evaluateValueParser = evaluateValueParser;
        }

        public bool CanHandle(IOperation operation) => operation is IBinaryOperation;

        public EvaluationResult? EvaluateBinaryOperation(BinaryOperationEvaluateContext context)
        {
            if (context.Operation is not IBinaryOperation binary || context.MethodGlobalStore is null)
                return null;

            var leftValue = _evaluateValueParser.GetValue(binary.LeftOperand, context.MethodGlobalStore);
            var rightValue = _evaluateValueParser.GetValue(binary.RightOperand, context.MethodGlobalStore);

            if (leftValue is UnknownValue || rightValue is UnknownValue)
            {
                return EvaluationResult.Unknown;
            }

            var evaluatedOpValueDict = new Dictionary<BinaryOperatorKind, bool>
            {
                { BinaryOperatorKind.Equals, Equals(leftValue, rightValue) },
                { BinaryOperatorKind.NotEquals, !Equals(leftValue, rightValue) },
                { BinaryOperatorKind.GreaterThan, _compareValueParser.CompareValues(leftValue, rightValue) > 0 },
                { BinaryOperatorKind.GreaterThanOrEqual, _compareValueParser.CompareValues(leftValue, rightValue) >= 0 },
                { BinaryOperatorKind.LessThan, _compareValueParser.CompareValues(leftValue, rightValue) < 0 },
                { BinaryOperatorKind.LessThanOrEqual, _compareValueParser.CompareValues(leftValue, rightValue) <= 0 }
            };

            if (evaluatedOpValueDict.TryGetValue(binary.OperatorKind, out var evaluatedOpValue))
            {
                return evaluatedOpValue ? EvaluationResult.True : EvaluationResult.False;
            }

            return EvaluationResult.Unknown;
        }
    }
}
