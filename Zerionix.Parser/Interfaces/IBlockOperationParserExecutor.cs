using Domain.Models;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser.Interfaces
{
    public interface IBlockOperationParserExecutor
    {
        void Execute(IOperation operation, MethodGlobalStore methodGlobalStore);
    }
}
