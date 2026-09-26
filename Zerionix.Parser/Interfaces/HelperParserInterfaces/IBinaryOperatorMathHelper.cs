using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Parser.Interfaces.HelperParserInterfaces
{
    public interface IBinaryOperatorMathHelper
    {
        object? CalculateValue(BinaryOperatorKind kind, object? leftVal, object? rightVal);
    }
}
