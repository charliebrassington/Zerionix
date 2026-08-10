using Zerionix.Domain.Models;
using Zerionix.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Zerionix.Service.RulesService
{
    public class RuleValidatorParserService : IRuleValidatorParserService
    {
        private readonly ILogger<RuleValidatorParserService> _logger;

        public RuleValidatorParserService(ILogger<RuleValidatorParserService> logger)
        {
            _logger = logger;
        }

        public List<LogicRule> ConvertToRules(List<string> unparsedRuleList)
        {
            var parsedRule = new List<LogicRule>();

            foreach (var unparsedRule in unparsedRuleList)
            {
                var ruleParts = unparsedRule.Split(" ");
                if (ruleParts.Length != 3)
                {
                    _logger.LogInformation("Unable to parse rule: {unparsedRule}", unparsedRule);
                    continue;
                }

                parsedRule.Add(new LogicRule
                {
                    VariableReference = ruleParts[0],
                    ComparisonOperation = ruleParts[1],
                    Value = ruleParts[2]
                });
            }

            return parsedRule;
        }
    }
}
