using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ParserResults
{
    public class SimpleAssignmentResult
    {
        public List<string>? LocalReferenceNameList { get; set; }
        public object? UnaryConstValue { get; set; }
        public string? PropertyRefName { get; set; }
    }
}
