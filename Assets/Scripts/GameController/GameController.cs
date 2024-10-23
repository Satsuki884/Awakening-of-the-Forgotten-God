using System.Collections;
using System.Collections.Generic;
using AFG.Character;
using AFG.MVP;
using UnityEngine;

namespace AFG
{
    public class GameController : MonoBehaviour
    {
        private static GameController _instance;
        public static GameController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameController>();
                    DontDestroyOnLoad(_instance);
                }
                return _instance;
            }
        }

        public SaveManager SaveManager { get;set;}
        public CombatModel CombatModel { get; set; }
        public PlayerCharactersHolderModel PlayerCharactersHolderModel { get; set; }
        public CharactersHolderModel CharactersHolderModel { get; set; }

        public void Initialize()
        {
            SaveManager = new SaveManager();
            CombatModel = new CombatModel();

            //TODO load from SaveManager
            PlayerCharactersHolderModel = new PlayerCharactersHolderModel(string.Empty);
            CharactersHolderModel = new CharactersHolderModel(string.Empty);
        }
    }
}

