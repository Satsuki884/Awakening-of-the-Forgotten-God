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
                    _instance.Initialize();
                    DontDestroyOnLoad(_instance);
                }
                return _instance;
            }
        }

        [SerializeField] private SaveManager _saveManager;
        public SaveManager SaveManager 
        {
            get => _saveManager;
            set => _saveManager = value;
        }
        
        public CombatModel CombatModel { get; set; }
        public PlayerCharactersHolderModel PlayerCharactersHolderModel { get; set; }
        public CharactersHolderModel CharactersHolderModel { get; set; }

        public void Initialize()
        {
            CombatModel = new CombatModel();

            //TODO load from SaveManager
            PlayerCharactersHolderModel = new PlayerCharactersHolderModel();
            CharactersHolderModel = new CharactersHolderModel();
        }
    }
}

