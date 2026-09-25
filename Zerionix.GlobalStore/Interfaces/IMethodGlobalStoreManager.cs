using Zerionix.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zerionix.GlobalStore.Interfaces
{
    public interface IMethodGlobalStoreManager
    {
        void AddSymbolicValue(List<SymbolicValue> symbolicValues, string keyToFetchFrom, string keyToStoreTo);
        void AddNewSymbolicValue(List<SymbolicValue> symbolicValues, string keyToStoreTo, object valueToStore);
    }
}
