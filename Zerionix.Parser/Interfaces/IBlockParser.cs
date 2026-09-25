using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;

namespace Zerionix.Parser.Interfaces
{
    public interface IBlockParser
    {
        bool CanHandle(BasicBlock block);
        void ParseBlock(BasicBlock block, MethodGlobalStore store);
    }
}
