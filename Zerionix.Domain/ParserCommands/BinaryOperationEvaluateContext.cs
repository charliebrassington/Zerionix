using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;

namespace Zerionix.Domain.ParserCommands
{
    public class BinaryOperationEvaluateContext : IParserCommand
    {
        public required IOperation Operation { get; set; }
        public EvaluationResult LeftEvaluationResult { get; set; } = EvaluationResult.Unknown;
        public EvaluationResult RightEvaluationResult { get; set; } = EvaluationResult.Unknown;
        public MethodGlobalStore? MethodGlobalStore { get; set; } = null;
    }
}
