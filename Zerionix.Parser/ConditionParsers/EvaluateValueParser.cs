using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;
using Zerionix.Parser.Interfaces.ConditionParserInterfaces;

namespace Zerionix.Parser.ConditionParsers
{
    public class EvaluateValueParser : IEvaluateValueParser
    {
        public object GetValue(IOperation operation, MethodGlobalStore methodGlobalStore)
        {
            if (operation is ILiteralOperation literal)
            {
                return literal.ConstantValue.HasValue ? literal.ConstantValue.Value! : null!;
            }
            if (operation is ILocalReferenceOperation local)
            {
                var result = methodGlobalStore.SymbolicValueList.FirstOrDefault(v => v.VariableReference == local.Local.Name);
                if (result != null)
                {
                    return result.Constant!;
                }

                return UnknownValue.Instance;
            }

            if (operation is IConversionOperation conversion)
            {
                return GetValue(conversion.Operand, methodGlobalStore);
            }

            if (operation.ConstantValue.HasValue)
            {
                return operation.ConstantValue.Value!;
            }

            return UnknownValue.Instance;
        }
    }
}
