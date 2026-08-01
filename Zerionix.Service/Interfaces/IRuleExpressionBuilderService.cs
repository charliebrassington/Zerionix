using Zerionix.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Service.Interfaces
{
    public interface IRuleExpressionBuilderService
    {
        string BuildRuleExpressions(List<LogicRule> ruleList, List<SymbolicValue> symbolicValues);
    }
}
