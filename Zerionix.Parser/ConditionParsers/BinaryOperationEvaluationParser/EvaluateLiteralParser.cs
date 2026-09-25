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
    public class EvaluateLiteralParser : IBinaryOperationEvaluationParser
    {
        public bool CanHandle(IOperation operation) => operation is ILiteralOperation;

        public EvaluationResult? EvaluateBinaryOperation(BinaryOperationEvaluateContext context)
        {
            if (context.Operation is not ILiteralOperation literal)
                return null;

            var value = literal.ConstantValue;

            if (value.HasValue && value.Value is bool boolean)
            {
                return boolean ? EvaluationResult.True : EvaluationResult.False;
            }

            return EvaluationResult.Unknown;
        }
    }
}
