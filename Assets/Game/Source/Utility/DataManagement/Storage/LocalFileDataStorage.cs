using System;
using System.Collections;
using System.IO;

namespace Game.Utility.DataManagement.Storage
{
    public class LocalFileDataStorage : IDataStorage
    {
        private readonly string _folderPath;
        private readonly string _saveFileExtension;

        public LocalFileDataStorage(string folderPath, string saveFileExtension)
        {
            _folderPath = folderPath;
            _saveFileExtension = saveFileExtension;
        }

        public IEnumerator Exists(string key, Action<bool> onExistResult)
        {
            bool exists = File.Exists(FulPath(key));

            onExistResult(exists);

            yield break;
        }

        public IEnumerator Read(string key, Action<string> onRead)
        {
            string text = File.ReadAllText(FulPath(key));

            onRead(text);

            yield break;
        }

        public IEnumerator Remove(string key)
        {
            File.Delete(FulPath(key));

            yield break;
        }

        public IEnumerator Write(string key, string serializedData)
        {
            File.WriteAllText(FulPath(key), serializedData);

            yield break;
        }

        private string FulPath(string key)
            => $"{Path.Combine(_folderPath, key)}.{_saveFileExtension}";
    }
}