using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;

namespace Zerionix.Parser.Interfaces.HelperParserInterfaces
{
    public interface IMathOperationValueHelper
    {
        object? GetOperationValue(IOperation operation, MethodGlobalStore methodGlobalStore);
    }
}
