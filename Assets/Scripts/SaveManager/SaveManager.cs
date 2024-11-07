
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
        [SerializeField] private CharacterDataHolder allCharactersDataHolder;
        [SerializeField] private CharacterDataHolder playerCharactersDataHolder;
        [SerializeField] private PlayerData playerDataDataHolder;

        private List<CharacterDataWrapper> _allCharactersDataWrapper;
        public List<CharacterDataWrapper> AllCharacters
        {
            get
            {
                if (_allCharactersDataWrapper == null ||
                   _allCharactersDataWrapper.Count == 0)
                {
                    _allCharactersDataWrapper =
                        LoadCharacter(allCharactersDataHolder, FilePathToAllCharacters);
                }

                return _allCharactersDataWrapper;
            }
        }

        //add set with validation etc
        private List<CharacterDataWrapper> _playerCharactersDataWrapper;
        public List<CharacterDataWrapper> PlayerCharacters
        {
            get
            {
                if (_playerCharactersDataWrapper == null ||
                   _playerCharactersDataWrapper.Count == 0)
                {
                    _playerCharactersDataWrapper =
                        LoadCharacter(playerCharactersDataHolder, FilePathToPlayerCharacters);
                }

                return _playerCharactersDataWrapper;
            }
        }

        private PlayerDataWrapper _playerData;
        public PlayerDataWrapper PlayerData
        {
            get
            {
                if (_playerData == null)
                {
                    _playerData = LoadPlayerData(playerDataDataHolder);
                }

                return _playerData;
            }

        }

        public static string FilePathToPlayerCharacters =>
            Path.Combine(Application.persistentDataPath, "PlayerCharacters.json");
        public static string FilePathToPlayerData =>
            Path.Combine(Application.persistentDataPath, "PlayerData.json");
        public static string FilePathToAllCharacters =>
            Path.Combine(Application.persistentDataPath, "AllCharacters.json");

        public void SavePlayerData(PlayerDataWrapper playerData)
        {
            if (!File.Exists(FilePathToPlayerData))
            {
                File.Create(FilePathToPlayerData).Dispose();
            }

            string json = JsonUtility.ToJson(new PlayerDataWrapper
            {
                PlayerName = playerData.PlayerName,
                CoinData = playerData.CoinData,
                BooksData = playerData.BooksData
            }, true);

            File.WriteAllText(FilePathToPlayerData, json);
        }

        private PlayerDataWrapper LoadPlayerData(PlayerData playerholder)
        {
            PlayerDataWrapper dataWrapper = null;

            //read file
            if (!File.Exists(FilePathToPlayerData))
            {
                string json = File.ReadAllText(FilePathToPlayerData);
                dataWrapper = JsonUtility.FromJson<PlayerDataWrapper>(json);
                return dataWrapper;
            }
            else
            {
                return LoadDefaultPlayerData(playerholder);
            }
        }

        private PlayerDataWrapper LoadDefaultPlayerData(PlayerData player)
        {
            PlayerDataWrapper dataWrapperNew = new PlayerDataWrapper
            {
                PlayerName = player.PlayerDataWrapper.PlayerName,
                CoinData = player.PlayerDataWrapper.CoinData,
                BooksData = player.PlayerDataWrapper.BooksData
            };

            //safe to file
            SavePlayerData(dataWrapperNew);

            string jsonNew = JsonUtility.ToJson(dataWrapperNew, true);
            //Debug.Log(jsonNew);

            return dataWrapperNew;
        }


        //use 2 principles 
        //DRY
        public void SavePurchaseCharacters(List<CharacterDataWrapper> characters)
        {
            SaveCharacters(characters, FilePathToPlayerCharacters);
        }
        public void SaveCharacters(List<CharacterDataWrapper> characters, string path)
        {
            if (File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            string json = JsonUtility.ToJson(new CharactersDataWrapper
            {
                characterDataWrappers = characters
            }, true);

            File.WriteAllText(path, json);
        }

        public void SynchronizePlayerCharactersHolders(List<CharacterDataWrapper> newPlayerCharacters)
        {
            var playerCharacterWrapersInHolder = playerCharactersDataHolder.
                CharacterData.
                Select(x => x.CharacterDataWrapper).ToList();

            if (playerCharacterWrapersInHolder.Count != newPlayerCharacters.Count &&
                !playerCharacterWrapersInHolder.SequenceEqual(newPlayerCharacters))
            {
                playerCharactersDataHolder.CharacterData.Clear();
                foreach (var characterData in allCharactersDataHolder.CharacterData)
                {
                    foreach (var newPlayerCharacter in newPlayerCharacters)
                    {
                        if (newPlayerCharacter.CharacterName.Equals(characterData.CharacterDataWrapper.CharacterName))
                        {
                            playerCharactersDataHolder.CharacterData.Add(characterData);
                            break;
                        }
                    }
                }
            }
        }

        public void SynchronizeAllCharactersHolders(CharacterDataWrapper updatedAllCharacter)
        {
            var allCharactersData = allCharactersDataHolder.
                CharacterData.ToList();

            foreach (var characterData in allCharactersData)
            {
                if (characterData.CharacterDataWrapper.CharacterName ==
                updatedAllCharacter.CharacterName &&
                characterData.CharacterDataWrapper.Level !=
                updatedAllCharacter.Level)
                {
                    characterData.CharacterDataWrapper.Level =
                        updatedAllCharacter.Level;
                    break;
                }
            }
        }

        public void LevelUpCharacter(List<CharacterDataWrapper> playerCharacters,
        CharacterDataWrapper allCharacters)
        {
            SaveCharacters(playerCharacters, FilePathToPlayerCharacters);
            SynchronizeAllCharactersHolders(allCharacters);

        }

        //TODO refactoring
        private List<CharacterDataWrapper> LoadCharacter(
            CharacterDataHolder dataHolder,
            string path)
        {
            CharactersDataWrapper dataWrapper = null;

            //read file
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Debug.Log(json);
                dataWrapper = JsonUtility.FromJson<CharactersDataWrapper>(json);

                int characterWrapperLenght = dataHolder.CharacterData.Count;
                int dataWraperLenght = dataWrapper.characterDataWrappers.Count;

                if (characterWrapperLenght == dataWraperLenght)
                {
                    for (int i = 0; i < dataHolder.CharacterData.Count; i++)
                    {
                        for (int j = 0; j < dataWrapper.characterDataWrappers.Count; j++)
                        {
                            if (dataHolder.CharacterData[i].CharacterDataWrapper.CharacterName.
                               Equals(dataWrapper.characterDataWrappers[j].CharacterName))
                            {
                                dataWrapper.characterDataWrappers[j].
                                    SetCharacterPrefab(dataHolder.CharacterData[i].CharacterDataWrapper.CharacterPrefab);

                                break;
                            }
                        }
                    }

                    return dataWrapper.characterDataWrappers;
                }
                return dataWrapper.characterDataWrappers;
            }
            else
            {
                return FillAllCharactersDefault(dataHolder, path);
            }
        }

        private List<CharacterDataWrapper> FillAllCharactersDefault(
            CharacterDataHolder dataHolder,
            string path)
        {
            CharactersDataWrapper dataWrapperNew = new CharactersDataWrapper
            {
                characterDataWrappers = dataHolder.
                    CharacterData.Select(x => x.CharacterDataWrapper).ToList()
            };

            //safe to file
            SaveCharacters(dataWrapperNew.characterDataWrappers, path);

            string jsonNew = JsonUtility.ToJson(dataWrapperNew, true);
            //Debug.Log(jsonNew);

            return dataWrapperNew.characterDataWrappers;
        }
    }
}
