using Zerionix.Domain.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Parser.Interfaces.BlockOperationParserInterfaces
{
    public interface IBlockOperationParser
    {
        bool CanHandle(IOperation operation);
        void ParseBlockOperation(IOperation operation, MethodGlobalStore store);
    }
}
