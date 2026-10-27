using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Domain.Models
{
    public class MethodCall
    {
        public required IMethodSymbol MethodSymbol { get; set; }
        public required List<MethodCallArgument> ArgumentList { get; set; }
        public object? ReturnValue { get; set; }
        public MethodReturn Return { get; set; } = new();
    }
}
