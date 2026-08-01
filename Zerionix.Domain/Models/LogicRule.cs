using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Domain.Models
{
    public class LogicRule
    {
        public required string VariableReference { get; set; }
        public required string ComparisonOperation { get; set; }
        public required string Value { get; set; }
    }
}
