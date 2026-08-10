using Zerionix.Domain.Models;
using Zerionix.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Zerionix.Service.RulesService
{
    public class RuleExpressionBuilderService : IRuleExpressionBuilderService
    {
        private readonly ILogger<RuleExpressionBuilderService> _logger;

        public RuleExpressionBuilderService(ILogger<RuleExpressionBuilderService> logger)
        {
            _logger = logger;
        }

        public string BuildRuleExpressions(List<LogicRule> ruleList, List<SymbolicValue> symbolicValues)
        {
            var expressions = new List<string>();
            foreach (var rule in ruleList)
            {
                var symbolicValue = symbolicValues.FirstOrDefault(s => s.VariableReference == rule.VariableReference);

                if (symbolicValue == null)
                {
                    _logger.LogInformation("Failed to find {rule.VariableReference} this might be related to unsupported features or does not exist within the checked scope", rule.VariableReference);
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
