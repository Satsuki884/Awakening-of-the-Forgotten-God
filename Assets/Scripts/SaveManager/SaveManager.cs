using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace AFG
{
    //TODO cashe character data saves
    //this is Facade
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private CharacterDataWrapperHolder characterDataWrapperHolder;
        public CharacterDataWrapperHolder CharacterDataWrapperHolder => characterDataWrapperHolder;
        
        private string _filePathToAllCharacters;
        private string _filePathToPlayerCharacters;
        
        public void Awake()
        {
            _filePathToAllCharacters = Path.Combine(Application.persistentDataPath, "AllCharacters.json");
            _filePathToPlayerCharacters = Path.Combine(Application.persistentDataPath, "PlayerCharacters.json");
        }

        public void SaveAllCharacterNames(List<CharacterDataWrapper> characters, string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            string json = JsonUtility.ToJson(new CharactersDataWrapper
            {
                characterDataWrappers = characters
            });
            
            File.WriteAllText(path, json);
        }

        //TODO refactoring
        public CharactersDataWrapper LoadAllCharacterNames()
        {
            //read file
            if (File.Exists(_filePathToAllCharacters))
            {
                string json = File.ReadAllText(_filePathToAllCharacters);
                CharactersDataWrapper dataWrapper = JsonUtility.FromJson<CharactersDataWrapper>(json);
                
                for(int i=0;i<characterDataWrapperHolder.CharacterDataWrappers.Length;i++)
                {
                   for(int j=0;j<dataWrapper.characterDataWrappers.Count;j++)
                   {
                       if(characterDataWrapperHolder.CharacterDataWrappers[i].CharacterName.
                           Equals(dataWrapper.characterDataWrappers[j].CharacterName))
                       {
                           dataWrapper.characterDataWrappers[j].
                               SetCharacterPrefab(characterDataWrapperHolder.CharacterDataWrappers[i].CharacterPrefab);
                           
                            break;
                       }
                   }
                }
                
                return dataWrapper;
            }
            //there are no file
            else
            {
                CharactersDataWrapper dataWrapper = new CharactersDataWrapper
                {
                    characterDataWrappers = characterDataWrapperHolder.
                        CharacterDataWrappers.ToList()
                };
                
                //safe to file
                SaveAllCharacterNames(dataWrapper.characterDataWrappers, _filePathToAllCharacters);
                
                string json = JsonUtility.ToJson(dataWrapper);
                Debug.Log(json);
                
                return dataWrapper;
            }
        }
        
        //TODO add realisation for player name saves
        public CharactersDataWrapper LoadPlayerCharacterNames()
        {
            return LoadAllCharacterNames();
        }
    }
}