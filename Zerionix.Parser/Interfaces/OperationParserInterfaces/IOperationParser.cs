using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Parser.Interfaces.OperationParserInterfaces
{
    public interface IOperationParser<Operation, Result>
    {
        Result ParseOperation(Operation operation);
    }
}
