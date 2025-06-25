using System;
using System.Collections;

namespace Game.Utility.DataManagement.Storage
{
    public interface IDataStorage
    {
        IEnumerator Read(string key, Action<string> onRead);
        IEnumerator Write(string key, string serializedData);
        IEnumerator Remove(string key);
        IEnumerator Exists(string key, Action<bool> onExistResult);
    }
}