using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;
using Zerionix.Domain.ParserResults;
using Zerionix.Parser.Interfaces.HelperParserInterfaces;
using Zerionix.Parser.Interfaces.OperationParserInterfaces;

namespace Zerionix.Parser.OperationParsers
{
    public class BinaryOperationMathParser : IOperationParser<IBinaryOperation, BinaryOperationMathResult>
    {
        private readonly IBinaryOperatorMathHelper _binaryOperatorMathHelper;

        public BinaryOperationMathParser(IBinaryOperatorMathHelper binaryOperatorMathHelper)
        {
            _binaryOperatorMathHelper = binaryOperatorMathHelper;
        }

        public BinaryOperationMathResult ParseOperation(IBinaryOperation operation, MethodGlobalStore methodGlobalStore)
        {
            return new BinaryOperationMathResult { Value = GetOperationValue(operation, methodGlobalStore) };
        }

        public object? GetOperationValue(IOperation operation, MethodGlobalStore methodGlobalStore)
        {
            return operation switch
            {
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
