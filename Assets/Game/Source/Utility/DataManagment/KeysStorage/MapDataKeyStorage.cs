using System;
using System.Collections.Generic;

namespace Game.Utility.DataManagment.KeysStorage
{
    public class MapDataKeyStorage : IDataKeyStarage
    {
        private readonly Dictionary<Type, string> _keys;

        public MapDataKeyStorage(Dictionary<Type, string> keys)
        {
            _keys = keys;
        }

        public string GetKeyFor<TData>() where TData : ISaveData
            => _keys[typeof(TData)];
    }
}