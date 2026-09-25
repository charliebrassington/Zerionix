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
    public class EvaluateAndOrParser : IBinaryOperationEvaluationParser
    {
        public bool CanHandle(IOperation operation) => operation is IBinaryOperation;

        public EvaluationResult? EvaluateBinaryOperation(BinaryOperationEvaluateContext context)
        {
            if (context.Operation is not IBinaryOperation binaryOperation)
                return null;

            var firstCheck = (int)binaryOperation.OperatorKind - (int)BinaryOperatorKind.ConditionalAnd;
            if (firstCheck > 1 || firstCheck < 0)
                return null;

            var firstCheckEvalResult = (EvaluationResult)binaryOperation.OperatorKind;
            var secondEvalResult = firstCheckEvalResult == EvaluationResult.True ? EvaluationResult.False : EvaluationResult.True;

            if (context.LeftEvaluationResult == firstCheckEvalResult || context.RightEvaluationResult == firstCheckEvalResult)
            {
                return firstCheckEvalResult;
            }

            if (context.LeftEvaluationResult == secondEvalResult && context.RightEvaluationResult == secondEvalResult)
            {
                return secondEvalResult;
            }

            return EvaluationResult.Unknown;
        }
    }
}
