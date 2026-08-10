using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Service.Interfaces
{
    public interface IRuleRunnerCompilerService
    {
        ScriptRunner<Dictionary<string, bool>> CompileRuleRunner(string ruleExpressionScript);
    }
}
