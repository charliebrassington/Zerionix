using Domain.Models;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CSharp.RuntimeBinder;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Service.RulesService
{
    public class RuleRunnerCompilerService : IRuleRunnerCompilerService
    {
        public ScriptRunner<Dictionary<string, bool>> CompileRuleRunner(string ruleExpressionScript)
        {
            var references = new[]
            {
                typeof(object).Assembly,
                typeof(Dictionary<string, bool>).Assembly,
                typeof(System.Runtime.CompilerServices.DynamicAttribute).Assembly,
                typeof(CSharpArgumentInfo).Assembly,
                typeof(ExpandoObject).Assembly
            };

            var options = ScriptOptions.Default
                .WithReferences(references)
                .WithImports("System", "System.Collections.Generic");

            var script = CSharpScript.Create<Dictionary<string, bool>>(ruleExpressionScript, options, globalsType: typeof(VariableRuntimeStore));

            return script.CreateDelegate();
        }
    }
}
