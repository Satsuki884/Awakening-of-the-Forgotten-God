using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace AFG
{
    public class SaveManager
    {
        private string _filePath;

        public SaveManager()
        {
            _filePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        }

        public void SaveCharacterNames(List<string> characterNames)
        {
            string json = JsonUtility.ToJson(new CharacterNamesWrapper { CharacterNames = characterNames });
            File.WriteAllText(_filePath, json);
        }

        public List<string> LoadCharacterNames()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                CharacterNamesWrapper wrapper = JsonUtility.FromJson<CharacterNamesWrapper>(json);
                return wrapper.CharacterNames;
            }
            return new List<string>();
        }

        [System.Serializable]
        private class CharacterNamesWrapper
        {
            public List<string> CharacterNames;
        }
    }
}