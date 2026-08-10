using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Service.Interfaces
{
    public interface IExecutorService
    {
        Task Execute(string workspacePath, List<string> ruleList);
    }
}
