using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Domain.Models
{
    public class UnknownValue
    {
        public static readonly UnknownValue Instance = new();

        private UnknownValue()
        {
        }
    }
}
