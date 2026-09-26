using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Parser.Interfaces.HelperParserInterfaces;

namespace Zerionix.Parser.HelperParsers
{
    public class BinaryOperatorMathHelper : IBinaryOperatorMathHelper
    {
        public object? CalculateValue(BinaryOperatorKind kind, object? leftVal, object? rightVal)
        {
            dynamic? dynamicLeft = leftVal;
            dynamic? dynamicRight = rightVal;

            var binaryOpMathDict = new Dictionary<BinaryOperatorKind, object?>
            {
                { BinaryOperatorKind.Add, dynamicLeft + dynamicRight },
                { BinaryOperatorKind.Subtract, dynamicLeft - dynamicRight  },
                { BinaryOperatorKind.Multiply, dynamicLeft * dynamicRight  },
                { BinaryOperatorKind.Divide, dynamicLeft is int && dynamicRight is int ? (double)dynamicLeft / (double)dynamicRight : dynamicLeft / dynamicRight  },
                { BinaryOperatorKind.IntegerDivide , (long)dynamicLeft % (long)dynamicRight  },
                { BinaryOperatorKind.Remainder, dynamicLeft % dynamicRight  },
            };

            return binaryOpMathDict.GetValueOrDefault(kind);
        }
    }
}
