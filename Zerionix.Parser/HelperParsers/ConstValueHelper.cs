using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;
using Zerionix.Parser.Interfaces.HelperParserInterfaces;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;

namespace Zerionix.Parser.HelperParsers
{
    public class ConstValueHelper : IConstValueHelper
    {
        private readonly IMathOperationValueHelper _mathOperationValueHelper;

        public ConstValueHelper(IMathOperationValueHelper mathOperationValueHelper)
        {
            _mathOperationValueHelper = mathOperationValueHelper;
        }

        public object? GetConstValue(IOperation operation, MethodGlobalStore methodGlobalStore)
        {
            if (operation is IBinaryOperation binaryOperation)
            {
                return _mathOperationValueHelper.GetOperationValue(binaryOperation, methodGlobalStore);
            }

            if (operation is IUnaryOperation unaryOperation)
            {
                return unaryOperation.ConstantValue.Value;
            }

            if (operation is ILiteralOperation literalOperation)
            {
                return literalOperation.ConstantValue.Value;
            }

            return null;
        }
    }
}
