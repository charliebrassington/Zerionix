using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.FlowAnalysis;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Zerionix.Domain.Models
{
    public class BlockConstraint
    {
        public BasicBlock? BlockTo { get; set; }
        public BasicBlock? BlockFrom { get; set; }
        public bool HasPassedConstaint { get; set; }
    }
}
