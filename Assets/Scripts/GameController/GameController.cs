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

        [SerializeField] private LevelModel _levelModel;
        public LevelModel LevelModel
        {
            get => _levelModel;
            set => _levelModel = value;
        }
        
        public CombatModel CombatModel { get; set; }

        [Header("Models")]
        [SerializeField] private PlayerCharactersHolderModel _playerCharactersHolderModel;
        public PlayerCharactersHolderModel PlayerCharactersHolderModel
        {
            get => _playerCharactersHolderModel;
            set => _playerCharactersHolderModel = value;
        }
        
        [SerializeField] private CharactersHolderModel _charactersHolderModel;
        public CharactersHolderModel CharactersHolderModel
        {
            get => _charactersHolderModel;
            set => _charactersHolderModel = value;
        }

        public void Initialize()
        {
            CombatModel = new CombatModel();
        }
    }
}

