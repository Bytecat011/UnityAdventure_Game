using System;
using System.Collections;
using Game.Utility.DataManagement.KeysStorage;
using Game.Utility.DataManagement.Serializers;
using Game.Utility.DataManagement.Storage;

namespace Game.Utility.DataManagement
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IDataSerializer _serializer;
        private readonly IDataKeyStarage _keyStorage;
        private readonly IDataStorage _storage;

        public SaveLoadService(IDataSerializer serializer, IDataKeyStarage keyStorage, IDataStorage storage)
        {
            _serializer = serializer;
            _keyStorage = keyStorage;
            _storage = storage;
        }

        public IEnumerator Exists<TData>(Action<bool> onExistsResult) where TData : ISaveData
        {
            string key = _keyStorage.GetKeyFor<TData>();

            yield return _storage.Exists(key, result => onExistsResult(result));
        }

        public IEnumerator Load<TData>(Action<TData> onLoad) where TData : ISaveData
        {
            string key = _keyStorage.GetKeyFor<TData>();

            string serializedData = "";

            yield return _storage.Read(key, result => serializedData = result);

            TData data = _serializer.Deserialize<TData>(serializedData);

            onLoad?.Invoke(data);
        }

        public IEnumerator Remove<TData>() where TData : ISaveData
        {
            string key = _keyStorage.GetKeyFor<TData>();

            yield return _storage.Remove(key);
        }

        public IEnumerator Save<TData>(TData data) where TData : ISaveData
        {
            string serializedData = _serializer.Serialize(data);
            string key = _keyStorage.GetKeyFor<TData>();
            yield return _storage.Write(key, serializedData);
        }
    }
}