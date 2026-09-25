using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Domain.Models
{
    public class MethodGlobalStore
    {
        public required string TracebackReference { get; set; }
        public List<SymbolicValue> SymbolicValueList { get; set; } = new();
        public List<BlockConstraint> BlockConstraints { get; set; } = new();
    }
}
