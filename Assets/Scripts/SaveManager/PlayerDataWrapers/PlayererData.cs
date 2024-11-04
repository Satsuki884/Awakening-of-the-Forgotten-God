using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CharacterController = AFG.Character.CharacterController;

namespace AFG
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Configs/new PlayerData")]
    public class PlayererData : ScriptableObject
    {
        [SerializeField] private PlayerDataWrapper _playerDataWrapper;
        public PlayerDataWrapper PlayerDataWrapper => _playerDataWrapper;

        
    }
    

    [System.Serializable]
    public class PlayerDataWrapper
    {

        [SerializeField] private CoinData _coinData;
        public CoinData CoinData
        {
            get=> _coinData;
            set => _coinData = value;
        }
        [SerializeField] private BooksData _bookData;
        public BooksData BooksData
        {
            get=> _bookData;
            set => _bookData = value;
        }

        [SerializeField] private string _playerName;
        public string PlayerName{
            get=> _playerName;
            set => _playerName = value;
        }

        /*[Button("SynchronizeFilePlayerData")]
        public void SynchronizeFilePlayerData()
        {
            var path = SaveManager.FilePathToPlayerData;
            
           if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            string json = JsonUtility.ToJson(new PlayerDataWrapper
            {
                PlayerName = PlayerName,
                CoinData = CoinData,
                BooksData = BooksData
            }, true);

            File.WriteAllText(path, json);
            
            Debug.Log("Synchronize File Data " + path);
        }*/
    }

    
}