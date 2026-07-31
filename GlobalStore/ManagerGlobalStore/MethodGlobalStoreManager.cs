using Domain.Models;
using GlobalStore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalStore.ManagerGlobalStore
{
    public class MethodGlobalStoreManager : IMethodGlobalStoreManager
    {
        public void AddSymbolicValue(List<SymbolicValue> symbolicValues, string keyToFetchFrom, string keyToStoreTo)
        {
            var value = symbolicValues.FirstOrDefault(s => s.VariableReference == keyToFetchFrom);

            if (value != null)
            {
                symbolicValues.Add(new SymbolicValue
                {
                    VariableReference = keyToStoreTo,
                    Constant = value.Constant
                });
            }
        }
    }
}
