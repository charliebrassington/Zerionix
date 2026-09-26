using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Parser.Interfaces.HelperParserInterfaces
{
    public interface IVariableNameHelper
    {
        string? GetVariableName(IOperation operation);
    }
}
