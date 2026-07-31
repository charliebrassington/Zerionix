using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalStore.Interfaces
{
    public interface IMethodGlobalStoreManager
    {
        void AddSymbolicValue(List<SymbolicValue> symbolicValues, string keyToFetchFrom, string keyToStoreTo);
    }
}
