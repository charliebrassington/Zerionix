using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;

namespace Zerionix.Parser.Interfaces.ConditionParserInterfaces
{
    public interface IConditionEvaluatorParser
    {
        EvaluationResult Evaluate(IOperation operation, MethodGlobalStore methodGlobalStore);
    }
}
