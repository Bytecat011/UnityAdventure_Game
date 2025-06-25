using System;
using System.Collections;
using UnityEngine;

namespace Game.Utility.DataManagement.Storage
{
    public class PlayerPrefsDataStorage : IDataStorage
    {
        public IEnumerator Exists(string key, Action<bool> onExistResult)
        {
            bool exists = PlayerPrefs.HasKey(key);

            onExistResult(exists);

            yield break;
        }

        public IEnumerator Read(string key, Action<string> onRead)
        {
            string text = PlayerPrefs.GetString(key);

            onRead(text);

            yield break;
        }

        public IEnumerator Remove(string key)
        {
            PlayerPrefs.DeleteKey(key);

            yield break;
        }

        public IEnumerator Write(string key, string serializedData)
        {
            PlayerPrefs.SetString(key, serializedData);

            yield break;
        }
    }
}