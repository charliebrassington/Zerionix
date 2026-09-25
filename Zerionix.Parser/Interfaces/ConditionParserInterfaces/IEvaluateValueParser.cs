using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;

namespace Zerionix.Parser.Interfaces.ConditionParserInterfaces
{
    public interface IEvaluateValueParser
    {
        object GetValue(IOperation operation, MethodGlobalStore methodGlobalStore);
    }
}
