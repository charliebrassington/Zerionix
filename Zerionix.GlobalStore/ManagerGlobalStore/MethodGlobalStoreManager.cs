using Zerionix.Domain.Models;
using Zerionix.GlobalStore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.GlobalStore.ManagerGlobalStore
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

        public void AddNewSymbolicValue(List<SymbolicValue> symbolicValues, string keyToStoreTo, object valueToStore)
        {
            var existingSymbolicValue = symbolicValues.FirstOrDefault(s => s.VariableReference == keyToStoreTo);

            if (existingSymbolicValue == null)
            {
                symbolicValues.Add(new SymbolicValue { VariableReference = keyToStoreTo, Constant = valueToStore});
            }
            else
            {
                existingSymbolicValue.Constant = valueToStore;
            }
        }
    }
}
