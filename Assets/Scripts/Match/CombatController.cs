using System.Collections.Generic;
using System.Linq;
using AFG.Character;
using AFG.Squad;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using CharacterController = AFG.Character.CharacterController;

namespace AFG.Combat
{
    public class CombatController : MonoBehaviour
    {
        [SerializeField] private SquadController _playerSquad;
        [SerializeField] private SquadController _aiSquad;

        [Header("Buttons")] 
        [SerializeField] private Button _attackMeleButton;
        [SerializeField] private Button _attackRangeButton;
        [SerializeField] private Button _attackAreaButton;
        [SerializeField] private Button _healButton;
        [SerializeField] private Button _bufButton;
        [SerializeField] private Button _debuffButton;
        [SerializeField] private Button _ultaButton;
        
        //TODO change after buf-debuf
        private List<CharacterController> _charactersQueue;

        private CharacterController _selectedCharacter;
        
        private void Start()
        {
            _playerSquad.Initialization();
            _aiSquad.Initialization();

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
            SelectCharacter(_charactersQueue[0]);
        }

        private void SelectCharacter(CharacterController character)
        {
            DeactivateAllButton();
            
            _selectedCharacter = character;
            _selectedCharacter.SelectCharacter();

            ActivateButtons();
        }

        private void ActivateButtons()
        {
            for (int i = 0; i < _selectedCharacter.Skills.Length; i++)
            {
                var skill = _selectedCharacter.Skills[i];
                //TODO add other skills buttons
                if (skill is CharacterMeleSkill)
                {
                    _attackMeleButton.gameObject.SetActive(true);
                    _attackMeleButton.onClick.AddListener(() =>
                    {
                        skill.UseSkill(_selectedCharacter, _aiSquad.Characters);
                    });
                }
            }
        }
        
        //TODO deactivate all buttons
        private void DeactivateAllButton()
        {
            
        }
    }
}