using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class VariableRuntimeStore
    {
        public Dictionary<string, object?> Variables { get; set; } = new();
    }
}
