using Domain.Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.RulesService
{
    public class RuleValidatorParserService : IRuleValidatorParserService
    {
        public List<LogicRule> ConvertToRules(List<string> unparsedRuleList)
        {
            var parsedRule = new List<LogicRule>();

            foreach (var unparsedRule in unparsedRuleList)
            {
                var ruleParts = unparsedRule.Split(" ");
                if (ruleParts.Length != 3)
                {
                    Console.WriteLine($"Unable to parse {unparsedRule}");
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
