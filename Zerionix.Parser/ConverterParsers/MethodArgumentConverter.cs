using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;

namespace Zerionix.Parser.ConverterParsers
{
    public static class MethodArgumentConverter
    {
        public static void AddArgumentsToMethodStore(MethodGlobalStore methodGlobalStore, List<MethodCallArgument> argumentList)
        {
            methodGlobalStore.SymbolicValueList.AddRange(argumentList.Select(a => new SymbolicValue() 
            { 
                VariableReference = a.ParameterName ?? "",
                Constant = a.ParamterValue
            }));
        }
    }
}
