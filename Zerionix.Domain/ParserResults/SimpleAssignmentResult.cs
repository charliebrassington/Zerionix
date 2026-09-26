using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Domain.ParserResults
{
    public class SimpleAssignmentResult
    {
        public List<string> LocalReferenceNameList { get; set; } = new();
        public object? UnaryConstValue { get; set; }
        public string? PropertyRefName { get; set; }
        public string? TargetName { get; set; }
        public string? VariableName { get; set; }
        public object? VariableValue { get; set; }
    }
}
