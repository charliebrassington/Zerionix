using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;
using Zerionix.Domain.ParserCommands;
using Zerionix.Parser.ConditionParsers;

namespace Zerionix.Parser.Interfaces.ConditionParserInterfaces
{
    public interface IBinaryOperationEvaluationParser
    {
        bool CanHandle(IOperation operation);
        EvaluationResult? EvaluateBinaryOperation(BinaryOperationEvaluateContext context);
    }
}
