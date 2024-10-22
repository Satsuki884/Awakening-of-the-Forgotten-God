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

        [Header("Buttons")] 
        [SerializeField] private Button _attackMeleButton;
        [SerializeField] private Button _attackRangeButton;
        [SerializeField] private Button _attackAreaButton;
        [SerializeField] private Button _healButton;
        [SerializeField] private Button _bufButton;
        [SerializeField] private Button _debuffButton;
        
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
           SelectCharacter(sortedCharacters[0]);

            /*foreach (var character in _playerSquad.Characters)
            {
                SelectCharacter(character);
            }*/
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
                        DeactivateAllButton();
                    });
                    
                }
                if (skill is CharacterRangeSkill)
                {
                    _attackRangeButton.gameObject.SetActive(true);
                    _attackRangeButton.onClick.AddListener(() =>
                    {
                        skill.UseSkill(_selectedCharacter, _aiSquad.Characters);
                        DeactivateAllButton();
                    });
                    
                }
                if (skill is CharacterBufSkill)
                {
                    _bufButton.gameObject.SetActive(true);
                    _bufButton.onClick.AddListener(() =>
                    {
                        skill.UseSkill(_selectedCharacter, _playerSquad.Characters);
                        DeactivateAllButton();
                    });

                }
                if (skill is CharacterDebufSkill)
                {
                    _debuffButton.gameObject.SetActive(true);
                    _debuffButton.onClick.AddListener(() =>
                    {
                        skill.UseSkill(_selectedCharacter, _aiSquad.Characters);
                        DeactivateAllButton();
                    });

                }
                if (skill is CharacterHealSkill)
                {
                    _healButton.gameObject.SetActive(true);
                    _healButton.onClick.AddListener(() =>
                    {
                        skill.UseSkill(_selectedCharacter, _playerSquad.Characters);
                        DeactivateAllButton();
                    });

                }
            }
        }


        //TODO deactivate all buttons
        private void DeactivateAllButton()
        {
            _attackMeleButton.gameObject.SetActive(false);
            _attackRangeButton.gameObject.SetActive(false);
            _attackAreaButton.gameObject.SetActive(false);
            _healButton.gameObject.SetActive(false);
            _bufButton.gameObject.SetActive(false);
            _debuffButton.gameObject.SetActive(false);
        }
    }
}