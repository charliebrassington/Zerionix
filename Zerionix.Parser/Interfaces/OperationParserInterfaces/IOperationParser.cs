using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Interfaces.OperationParserInterfaces
{
    public interface IOperationParser<Operation, Result>
    {
        Result ParseOperation(Operation operation);
    }
}
