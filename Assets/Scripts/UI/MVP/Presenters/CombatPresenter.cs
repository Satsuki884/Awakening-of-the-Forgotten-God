using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AFG.Character;
using AFG.Squad;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = AFG.Character.CharacterController;

namespace AFG.MVP
{
    public class CombatPresenter : MonoBehaviour
    {
        [SerializeField] private Button _attackMeleButton;
        [SerializeField] private Button _attackRangeButton;
        [SerializeField] private Button _attackAreaButton;
        [SerializeField] private Button _healButton;
        [SerializeField] private Button _bufButton;
        [SerializeField] private Button _debuffButton;
        [SerializeField] private TextMeshProUGUI _currentCharacter;

        public void Start()
        {
            GameController.Instance.CombatModel.OnCharacterSelected += OnCharacterSelected;
        }

        public void OnDestroy()
        {
            GameController.Instance.CombatModel.OnCharacterSelected -= OnCharacterSelected;
        }

        private void OnCharacterSelected(CharacterController selectedCharacter)
        {
            if (selectedCharacter != null)
            {
                DeactivateAllButton();

                string fullName = selectedCharacter.name.ToString();
                string firstWord = fullName.Split(' ')[0];
                _currentCharacter.text = firstWord;

                ActivateButtons(selectedCharacter);
            }
        }

        SquadController tempAISquad;
        SquadController tempPlayerSquad;

        private void ActivateButtons(CharacterController selectedCharacter)
        {
            SquadController parent = selectedCharacter.GetComponentInParent<SquadController>();

            //TODO implement brain check
            if (parent.name == "SquadPlayerController")
            {
                tempAISquad = GameController.Instance.CombatModel.AiSquad;
                tempPlayerSquad = GameController.Instance.CombatModel.PlayerSquad;
            }
            else if (parent.name == "SquadAIController")
            {
                tempAISquad = GameController.Instance.CombatModel.PlayerSquad;
                tempPlayerSquad = GameController.Instance.CombatModel.AiSquad;
            }


            for (int i = 0; i < selectedCharacter.Skills.Length; i++)
            {
                var skill = selectedCharacter.Skills[i];

                if (skill is CharacterMeleSkill)
                {
                    AddActionToButton(_attackMeleButton,
                        selectedCharacter,
                        skill,
                        tempAISquad.Characters);
                }
                else if (skill is CharacterRangeSkill)
                {
                    AddActionToButton(_attackMeleButton,
                        selectedCharacter,
                        skill,
                        tempAISquad.Characters);
                }
                else if (skill is CharacterBufSkill)
                {
                    AddActionToButton(_bufButton,
                        selectedCharacter,
                        skill,
                        tempPlayerSquad.Characters);
                }
                else if (skill is CharacterDebufSkill)
                {
                    AddActionToButton(_debuffButton,
                        selectedCharacter,
                        skill,
                        tempAISquad.Characters);
                }
                else if (skill is CharacterHealSkill)
                {
                    AddActionToButton(_healButton,
                        selectedCharacter,
                        skill,
                        tempPlayerSquad.Characters);
                }
                else if (skill is CharacterAreaDamageSkill)
                {
                    AddActionToButton(_attackAreaButton,
                        selectedCharacter,
                        skill,
                        tempAISquad.Characters);
                }
            }
        }

        //principle DRY (Don't Repeat Yourself)
        private void AddActionToButton(Button button,
            CharacterController selectedCharacter,
            CharacterSkill skill,
            List<CharacterController> characterTargets)
        {
            button.gameObject.SetActive(true);

            button.onClick.RemoveAllListeners();

            button.onClick.AddListener(() =>
            {
                selectedCharacter.SelectedCharacterSkill = skill;
                GameController.Instance.CombatModel.SelectedTargets = characterTargets;
                
                DeactivateAllButton();
            });
        }

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

