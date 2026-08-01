using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class SymbolicValue
    {
        public required string VariableReference { get; set; }
        public object? Constant { get; set; }
    }
}
