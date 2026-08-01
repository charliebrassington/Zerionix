using Zerionix.Domain.Models;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Parser.Interfaces
{
    public interface ITreeParser
    {
        List<MethodGlobalStore> ParseTrees(List<SyntaxTree> treeList);
    }
}
