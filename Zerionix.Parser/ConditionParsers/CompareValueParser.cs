using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Parser.Interfaces.ConditionParserInterfaces;

namespace Zerionix.Parser.ConditionParsers
{
    public class CompareValueParser : ICompareValueParser
    {
        public int CompareValues(object? left, object? right)
        {
            if (left is null || right is null)
                throw new InvalidOperationException("Cannot compare null using >, <, >= or <=");

            if (left is IComparable comparable)
            {
                try
                {
                    return comparable.CompareTo(right);
                }
                catch
                {
                    return 0;
                }
            }

            throw new InvalidOperationException($"Values of type {left.GetType()} cannot be compared.");
        }
    }
}
