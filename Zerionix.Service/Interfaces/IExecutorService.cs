using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface IExecutorService
    {
        Task Execute(List<SyntaxTree> treeList, List<string> ruleList);
    }
}
