
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
        [SerializeField] private CharacterDataHolder characterDataHolder;
        [SerializeField] private CharacterDataHolder playerCharacterDataHolder;
        
        public CharacterDataHolder CharacterDataHolder => characterDataHolder;
        public CharacterDataHolder PlayerCharacterDataHolder => playerCharacterDataHolder;
        
        private string _filePathToAllCharacters;
        private string _filePathToPlayerCharacters;
        
        public void Awake()
        {
            _filePathToAllCharacters = Path.Combine(Application.persistentDataPath, "AllCharacters.json");
            _filePathToPlayerCharacters = Path.Combine(Application.persistentDataPath, "PlayerCharacters.json");
        }

        //use 2 principles 
        //DRY
        public void SavePurchaseCharacters(List<CharacterDataWrapper> characters)
        {
            SaveCharacterNames(characters, _filePathToPlayerCharacters);
        }
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
            CharacterDataHolder dataHolder, 
            string path)
        {
            CharactersDataWrapper dataWrapper = null;

            //read file
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                dataWrapper = JsonUtility.FromJson<CharactersDataWrapper>(json);

                int characterWrapperLenght = dataHolder.CharacterData.Length;
                int dataWraperLenght = dataWrapper.characterDataWrappers.Count;

                if (characterWrapperLenght == dataWraperLenght)
                {
                    for(int i=0;i<dataHolder.CharacterData.Length;i++)
                    {
                        for(int j=0;j<dataWrapper.characterDataWrappers.Count;j++)
                        {
                            if(dataHolder.CharacterData[i].CharacterDataWrapper.CharacterName.
                               Equals(dataWrapper.characterDataWrappers[j].CharacterName))
                            {
                                dataWrapper.characterDataWrappers[j].
                                    SetCharacterPrefab(dataHolder.CharacterData[i].CharacterDataWrapper.CharacterPrefab);
                           
                                break;
                            }
                        }
                    }
                    
                    return dataWrapper;
                }
            }
            
            return FillAllCharactersDefault(dataHolder, path);
        }

        private CharactersDataWrapper FillAllCharactersDefault(
            CharacterDataHolder dataHolder, 
            string path)
        {
            CharactersDataWrapper dataWrapperNew = new CharactersDataWrapper
            {
                characterDataWrappers = dataHolder.
                    CharacterData.Select(x=>x.CharacterDataWrapper).ToList()
            };
            
            //safe to file
            SaveCharacterNames(dataWrapperNew.characterDataWrappers, path);
            
            string jsonNew = JsonUtility.ToJson(dataWrapperNew);
            Debug.Log(jsonNew);
            
            return dataWrapperNew;
        }
        
        public CharactersDataWrapper LoadPlayerCharacterNames()
        {
            return LoadCharacterNames(playerCharacterDataHolder, _filePathToPlayerCharacters);
        }

        //use for store
        public CharactersDataWrapper LoadAllCharacterNames()
        {
            return LoadCharacterNames(characterDataHolder, _filePathToAllCharacters);
        }
    }
}
