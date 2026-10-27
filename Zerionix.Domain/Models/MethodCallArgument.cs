using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Domain.Models
{
    public class MethodCallArgument
    {
        public string? ParameterName { get; set; }
        public object? ParamterValue { get; set; }
    }
}
