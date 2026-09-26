using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;

namespace Zerionix.Parser.Interfaces.OperationParserInterfaces
{
    public interface IOperationParser<Operation, Result>
    {
        Result ParseOperation(Operation operation, MethodGlobalStore methodGlobalStore);
    }
}
