using Zerionix.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Service.Interfaces
{
    public interface IRuleValidatorParserService
    {
        List<LogicRule> ConvertToRules(List<string> unparsedRuleList);
    }
}
