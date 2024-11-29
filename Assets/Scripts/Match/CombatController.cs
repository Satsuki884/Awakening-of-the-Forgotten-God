using System;
using System.Collections.Generic;
using System.Linq;
using AFG.Character;
using AFG.Squad;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using CharacterController = AFG.Character.CharacterController;

namespace AFG.Combat
{
    public class CombatController : MonoBehaviour
    {
        [SerializeField] private SquadController _playerSquad;
        [SerializeField] private SquadController _aiSquad;

        private List<CharacterController> _charactersQueue;

        private CharacterController _selectedCharacter;
        private int _currentCharacterIndex = 0;

        private int _aiCharactersCount;
        private int _playerCharactersCount;

        public CharacterDamageController characterDamageController;

        private void Start()
        {
            var playerSquadData = GameController.Instance.SaveManager.PlayerSquad;

            var aiSquadData = GameController.
                Instance.
                LevelModel.
                GetAiSquad();

            _playerSquad.Initialization(playerSquadData);
            _aiSquad.Initialization(aiSquadData.ToList());

            GameController.Instance.CombatModel.PlayerSquad = _playerSquad;
            GameController.Instance.CombatModel.AiSquad = _aiSquad;

            _charactersQueue = new List<CharacterController>();

            // Combine characters from both squads
            var allCharacters = _playerSquad.Characters.Concat(_aiSquad.Characters).ToList();

            // Sort characters by Speed in descending order
            var sortedCharacters = allCharacters.OrderByDescending(character => character.Speed).ToList();

            // Enqueue sorted characters
            foreach (var character in sortedCharacters)
            {
                _charactersQueue.Add(character);
            }

            SelectCharacter(sortedCharacters[_currentCharacterIndex]);

            characterDamageController.OnQueueUpdated += UpdateQueue;
        }

        private void OnDestroy()
        {
            characterDamageController.OnQueueUpdated -= UpdateQueue;
        }

        private void OnEnable()
        {
            if (GameController.Instance != null && GameController.Instance.CombatModel != null)
            {
                GameController.Instance.CombatModel.OnMoveFinished += SelectNextCharacter;
            }
            else
            {
                Debug.LogError("GameController or CombatModel is null.");
            }
        }

        private void OnDisable()
        {
            if (GameController.Instance != null && GameController.Instance.CombatModel != null)
            {
                GameController.Instance.CombatModel.OnMoveFinished -= SelectNextCharacter;
            }
        }

        private void SelectCharacter(CharacterController character)
        {
            Debug.Log("SelectCharacter: " + character.name);
            _selectedCharacter = character;
            _selectedCharacter.SelectCharacter();

            GameController.Instance.CombatModel.SelectedCharacter = _selectedCharacter;
        }



        private void SelectNextCharacter()
        {
            _currentCharacterIndex++;
            if (_currentCharacterIndex >= _charactersQueue.Count)
            {
                _currentCharacterIndex = 0;
            }
            SelectCharacter(_charactersQueue[_currentCharacterIndex]);
        }

        [SerializeField] private EndGameController _endgameController;

        public void UpdateQueue(CharacterController character)
        {
            Debug.Log("Update Queue. Will be deleted character with name: " + character.name);
            for (int i = 0; i < _charactersQueue.Count; i++)
            {
                int j = i;
                if (_charactersQueue[j] == character)
                {
                    _charactersQueue.RemoveAt(j);
                }
            }

            for (int i = 0; i < _charactersQueue.Count; i++)
            {
                if (_charactersQueue[i].Brain.Type == CharacterBrainType.AI)
                {
                    _aiCharactersCount++;
                }
                else if (_charactersQueue[i].Brain.Type == CharacterBrainType.Player)
                {
                    _playerCharactersCount++;
                }
            }

            if (_aiCharactersCount == 0)
            {
                Debug.Log("Player wins");
                _endgameController.EndLevel(true);
            }
            else if (_playerCharactersCount == 0)
            {
                Debug.Log("AI wins");
                _endgameController.EndLevel(false);
            }
            else
            {
                _aiCharactersCount = 0;
                _playerCharactersCount = 0;
            }


        }
    }
}