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
        //[SerializeField] private PlayerCharacterDataWrapperHolder playerCharacterDataWrapperHolder;
        public CharacterDataWrapperHolder CharacterDataWrapperHolder => characterDataWrapperHolder;
        //public PlayerCharacterDataWrapperHolder PlayerCharacterDataWrapperHolder => playerCharacterDataWrapperHolder;
        
        private string _filePathToAllCharacters;
        private string _filePathToPlayerCharacters;
        
        public void Awake()
        {
            _filePathToAllCharacters = Path.Combine(Application.persistentDataPath, "AllCharacters.json");
            _filePathToPlayerCharacters = Path.Combine(Application.persistentDataPath, "PlayerCharacters.json");
        }

        /*public void SavePlayerCharacterNames(List<PlayerCharacterDataWrapper> characters, string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            string json = JsonUtility.ToJson(new PlayerCharactersDataWrapper
            {
                playerCharacterDataWrappers = characters
            });

            File.WriteAllText(path, json);
        }*/

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
            //TODO add check for validation
            //read file
            if (File.Exists(_filePathToAllCharacters))
            {
                string json = File.ReadAllText(_filePathToAllCharacters);
                CharactersDataWrapper dataWrapper = JsonUtility.FromJson<CharactersDataWrapper>(json);

                int characterWrapperLenght = characterDataWrapperHolder.CharacterDataWrappers.Length;
                int dataWraperLenght = dataWrapper.characterDataWrappers.Count;

                if (characterWrapperLenght == dataWraperLenght)
                {
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
            }
            
            //there are no file
            CharactersDataWrapper dataWrapperNew = new CharactersDataWrapper
            {
                characterDataWrappers = characterDataWrapperHolder.
                    CharacterDataWrappers.ToList()
            };
            
            //safe to file
            SaveAllCharacterNames(dataWrapperNew.characterDataWrappers, _filePathToAllCharacters);
            
            string jsonNew = JsonUtility.ToJson(dataWrapperNew);
            Debug.Log(jsonNew);
            
            return dataWrapperNew;
        }
        
        //TODO add realisation for player name saves
        public CharactersDataWrapper LoadPlayerCharacterNames()
        {
            return LoadAllCharacterNames();
        }





        /*public PlayerCharactersDataWrapper LoadAllPlayerCharacterNames()
        {
            //read file
            if (File.Exists(_filePathToPlayerCharacters))
            {
                string json = File.ReadAllText(_filePathToPlayerCharacters);
                PlayerCharactersDataWrapper dataWrapper = JsonUtility.FromJson<PlayerCharactersDataWrapper>(json);

                for (int i = 0; i < playerCharacterDataWrapperHolder.PlayerCharacterDataWrappers.Length; i++)
                {
                    for (int j = 0; j < dataWrapper.playerCharacterDataWrappers.Count; j++)
                    {
                        if (playerCharacterDataWrapperHolder.PlayerCharacterDataWrappers[i].CharacterName.
                            Equals(dataWrapper.playerCharacterDataWrappers[j].CharacterName))
                        {
                            dataWrapper.playerCharacterDataWrappers[j].
                                SetCharacterPrefab(playerCharacterDataWrapperHolder.PlayerCharacterDataWrappers[i].CharacterPrefab);

                            break;
                        }
                    }
                }

                return dataWrapper;
            }
            //there are no file
            else
            {
                PlayerCharactersDataWrapper dataWrapper = new PlayerCharactersDataWrapper
                {
                    playerCharacterDataWrappers = playerCharacterDataWrapperHolder.
                        PlayerCharacterDataWrappers.ToList()
                };

                //safe to file
                SavePlayerCharacterNames(dataWrapper.playerCharacterDataWrappers, _filePathToPlayerCharacters);

                string json = JsonUtility.ToJson(dataWrapper);
                Debug.Log(json);

                return dataWrapper;
            }
        }

        //TODO add realisation for player name saves
        public PlayerCharactersDataWrapper LoadPlayerCharacterName()
        {
            return LoadAllPlayerCharacterNames();
        }*/
    }
}