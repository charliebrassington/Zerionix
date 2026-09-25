using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.Parser.Interfaces.ConditionParserInterfaces
{
    public interface ICompareValueParser
    {
        int CompareValues(object? left, object? right);
    }
}
