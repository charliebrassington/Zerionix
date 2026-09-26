using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;
using Zerionix.Parser.Interfaces.HelperParserInterfaces;

namespace Zerionix.Parser.HelperParsers
{
    public class MathOperationValueHelper : IMathOperationValueHelper
    {
        private readonly IBinaryOperatorMathHelper _binaryOperatorMathHelper;

        public MathOperationValueHelper(IBinaryOperatorMathHelper binaryOperatorMathHelper)
        {
            _binaryOperatorMathHelper = binaryOperatorMathHelper;
        }

        public object? GetOperationValue(IOperation operation, MethodGlobalStore methodGlobalStore)
        {
            return operation switch
            {
                ICompoundAssignmentOperation compoundAssign =>  _binaryOperatorMathHelper.CalculateValue(compoundAssign.OperatorKind, GetOperationValue(compoundAssign.Target, methodGlobalStore), GetOperationValue(compoundAssign.Value, methodGlobalStore)),
                ILocalReferenceOperation localRef => methodGlobalStore.SymbolicValueList.FirstOrDefault(s => s.VariableReference == localRef.Local.Name)?.Constant,
                IConversionOperation conversion => GetOperationValue(conversion.Operand, methodGlobalStore),
                ILiteralOperation literal when literal.ConstantValue.HasValue => literal.ConstantValue.Value,
                IBinaryOperation binary => _binaryOperatorMathHelper.CalculateValue(binary.OperatorKind, GetOperationValue(binary.LeftOperand, methodGlobalStore), GetOperationValue(binary.RightOperand, methodGlobalStore)),
                IUnaryOperation unary when unary.OperatorKind == UnaryOperatorKind.Minus => -((dynamic?)GetOperationValue(unary.Operand, methodGlobalStore)),
                _ => null
            };
        }
    }
}
