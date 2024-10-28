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
        [SerializeField] private CharacterDataWrapperHolder playerCharacterDataWrapperHolder;
        
        public CharacterDataWrapperHolder CharacterDataWrapperHolder => characterDataWrapperHolder;
        public CharacterDataWrapperHolder PlayerCharacterDataWrapperHolder => playerCharacterDataWrapperHolder;
        
        private string _filePathToAllCharacters;
        private string _filePathToPlayerCharacters;
        
        public void Awake()
        {
            _filePathToAllCharacters = Path.Combine(Application.persistentDataPath, "AllCharacters.json");
            _filePathToPlayerCharacters = Path.Combine(Application.persistentDataPath, "PlayerCharacters.json");
        }

        //use 2 principles 
        //DRY
        public void SaveCharacterNames(List<CharacterDataWrapper> characters, string path)
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
        public CharactersDataWrapper LoadCharacterNames(
            CharacterDataWrapperHolder dataWrapperHolder, 
            string path)
        {
            CharactersDataWrapper dataWrapper = null;

            //read file
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                dataWrapper = JsonUtility.FromJson<CharactersDataWrapper>(json);

                int characterWrapperLenght = dataWrapperHolder.CharacterDataWrappers.Length;
                int dataWraperLenght = dataWrapper.characterDataWrappers.Count;

                if (characterWrapperLenght == dataWraperLenght)
                {
                    for(int i=0;i<dataWrapperHolder.CharacterDataWrappers.Length;i++)
                    {
                        for(int j=0;j<dataWrapper.characterDataWrappers.Count;j++)
                        {
                            if(dataWrapperHolder.CharacterDataWrappers[i].CharacterName.
                               Equals(dataWrapper.characterDataWrappers[j].CharacterName))
                            {
                                dataWrapper.characterDataWrappers[j].
                                    SetCharacterPrefab(dataWrapperHolder.CharacterDataWrappers[i].CharacterPrefab);
                           
                                break;
                            }
                        }
                    }
                    
                    return dataWrapper;
                }
            }
            
            return FillAllCharactersDefault(dataWrapperHolder, path);
        }

        private CharactersDataWrapper FillAllCharactersDefault(
            CharacterDataWrapperHolder dataWrapperHolder, 
            string path)
        {
            CharactersDataWrapper dataWrapperNew = new CharactersDataWrapper
            {
                characterDataWrappers = dataWrapperHolder.
                    CharacterDataWrappers.ToList()
            };
            
            //safe to file
            SaveCharacterNames(dataWrapperNew.characterDataWrappers, path);
            
            string jsonNew = JsonUtility.ToJson(dataWrapperNew);
            Debug.Log(jsonNew);
            
            return dataWrapperNew;
        }
        
        public CharactersDataWrapper LoadPlayerCharacterNames()
        {
            return LoadCharacterNames(playerCharacterDataWrapperHolder, _filePathToPlayerCharacters);
        }

        //use for store
        public CharactersDataWrapper LoadAllCharacterNames()
        {
            return LoadCharacterNames(characterDataWrapperHolder, _filePathToAllCharacters);
        }
    }
}