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
    public class EvaluateLocalParser : IBinaryOperationEvaluationParser
    {
        public bool CanHandle(IOperation operation) => operation is ILocalReferenceOperation;

        public EvaluationResult? EvaluateBinaryOperation(BinaryOperationEvaluateContext context)
        {
            if (context.Operation is not ILocalReferenceOperation local || context.MethodGlobalStore is null)
                return null;

            var result = context.MethodGlobalStore.SymbolicValueList.FirstOrDefault(v => v.VariableReference == local.Local.Name);

            if (result == null)
                return EvaluationResult.Unknown;

            if (result.Constant is bool boolean)
            {
                return boolean ? EvaluationResult.True : EvaluationResult.False;
            }

            return EvaluationResult.Unknown;
        }
    }
}
