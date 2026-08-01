using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface IRuleValidatorParserService
    {
        List<LogicRule> ConvertToRules(List<string> unparsedRuleList);
    }
}
