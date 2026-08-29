using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Zerionix.Domain.Models
{
    public class BlockConstraint
    {
        public int BlockID { get; set; }
        public IOperation? Condition { get; set; }
        public bool HasToPassExpression { get; set; }
    }
}
