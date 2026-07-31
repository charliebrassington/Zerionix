using Domain.Models;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CSharp.RuntimeBinder;
using Service.Interfaces;
using System.Dynamic;

namespace Service.RulesService
{
    public class RulesService : IRulesService
    {
        private readonly IRuleExpressionBuilderService _ruleExpressionBuilderService;
        private readonly IRuleValidatorParserService _ruleValidatorParserService;
        private readonly IRuleRunnerCompilerService _ruleRunnerCompilerService;

        public RulesService(
            IRuleExpressionBuilderService ruleExpressionBuilderService, 
            IRuleValidatorParserService ruleValidatorParserService,
            IRuleRunnerCompilerService ruleRunnerCompilerService)
        {
            _ruleExpressionBuilderService = ruleExpressionBuilderService;
            _ruleValidatorParserService = ruleValidatorParserService;
            _ruleRunnerCompilerService = ruleRunnerCompilerService;
        }

        public async Task<Dictionary<string, bool>?> CheckRulesForViolations(List<string> ruleList, List<SymbolicValue> symbolicValues)
        {
            var parsedRules = _ruleValidatorParserService.ConvertToRules(ruleList);
            var expressionScript = _ruleExpressionBuilderService.BuildRuleExpressions(parsedRules, symbolicValues);

            var compiledRunner = _ruleRunnerCompilerService.CompileRuleRunner(expressionScript);

            var variableRuntimeStore = new VariableRuntimeStore();

            foreach (var rule in parsedRules)
            {
                var symbolicValue = symbolicValues.FirstOrDefault(s => s.VariableReference == rule.VariableReference);
                if (symbolicValue == null) continue;

                variableRuntimeStore.Variables[$"{rule.VariableReference}_value"] = symbolicValue.Constant;

                variableRuntimeStore.Variables[rule.VariableReference] = ParseTargetValue(rule.Value, symbolicValue.Constant);
            }

            return await compiledRunner(variableRuntimeStore);
        }

        private object? ParseTargetValue(string rawValue, object? templateConstant)
        {
            if (templateConstant == null || rawValue == null) return rawValue;
            return templateConstant switch
            {
                decimal => decimal.Parse(rawValue),
                int => int.Parse(rawValue),
                long => long.Parse(rawValue),
                double => double.Parse(rawValue),
                bool => bool.Parse(rawValue),
                _ => rawValue
            };
        }
    }
}
