using System;
using System.Collections.Generic;
using System.Text;
using Zerionix.Domain.Models;

namespace Zerionix.Parser.ConverterParsers
{
    public static class MethodReturnConverter
    {
        public static void AddReturnValuesToMethodStore(MethodGlobalStore methodGlobalStore, MethodReturn methodReturn)
        {
            if (methodGlobalStore.CurrentMethodCall == null)
                return;

            methodGlobalStore.CurrentMethodCall.Return.Value = methodReturn.Value;
            methodGlobalStore.CurrentMethodCall.Return.ObjectName = methodReturn.ObjectName;
        }
    }
}
