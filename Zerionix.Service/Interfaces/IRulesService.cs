using Zerionix.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Service.Interfaces
{
    public interface IRulesService
    {
        Task<Dictionary<string, bool>?> CheckRulesForViolations(List<string> ruleList, List<SymbolicValue> symbolicValues);
    }
}
