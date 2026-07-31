using Domain.Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.RulesService
{
    public class RuleExpressionBuilderService : IRuleExpressionBuilderService
    {
        public string BuildRuleExpressions(List<LogicRule> ruleList, List<SymbolicValue> symbolicValues)
        {
            var expressions = new List<string>();
            foreach (var rule in ruleList)
            {
                var symbolicValue = symbolicValues.FirstOrDefault(s => s.VariableReference == rule.VariableReference);

                if (symbolicValue == null)
                {
                    Console.WriteLine($"Failed to find {rule.VariableReference} might be related to unsupported features or does not exist within the checked scope");
                    continue;
                }
                var ruleStringKey = $"{rule.VariableReference} {rule.ComparisonOperation} {rule.Value}";
                string expression = $"{{ \"{ruleStringKey}\", (dynamic)Variables[\"{rule.VariableReference}_value\"] {rule.ComparisonOperation} (dynamic)Variables[\"{rule.VariableReference}\"] }}";
                expressions.Add(expression);
            }

            return $@"
                new Dictionary<string, bool> {{
                    {string.Join(",\n", expressions)}
                }}";
        }
    }
}
