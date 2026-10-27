using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Domain.Models
{
    public class MethodGlobalStore
    {
        public string? TracebackReference { get; set; }
        public int CurrentBlock { get; set; }
        public MethodCall? CurrentMethodCall { get; set; }
        public MethodParsingState MethodState { get; set; } = MethodParsingState.Continue;
        public List<SymbolicValue> SymbolicValueList { get; set; } = new();
        public List<BlockConstraint> BlockConstraints { get; set; } = new();
        public object? ReturnValue { get; set; }
        public MethodReturn ReturnValues { get; set; } = new();
    }
}
