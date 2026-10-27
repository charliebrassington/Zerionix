using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;

namespace Zerionix.Domain.ParserResults
{
    public class SimpleAssignmentResult
    {
        public string? TargetName { get; set; }
        public string? VariableName { get; set; }
        public object? VariableValue { get; set; }
        public MethodCall? MethodCallRequired { get; set; }
    }
}
