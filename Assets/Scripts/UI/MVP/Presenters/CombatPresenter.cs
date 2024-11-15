using System;
using System.Collections.Generic;
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

        public void OnEnable()
        {
            GameController.Instance.CombatModel.OnCharacterSelected += OnCharacterSelected;
        }

        public void OnDisable()
        {
            GameController.Instance.CombatModel.OnCharacterSelected -= OnCharacterSelected;
        }

        private void OnCharacterSelected(CharacterController _selectedCharacter)
        {
            DeactivateAllButton();
            if (_selectedCharacter != null)
            {
                HighlightCharacter(_selectedCharacter, false);
            }
            HighlightCharacter(_selectedCharacter, true);
            string fullName = _selectedCharacter.name.ToString();
            string firstWord = fullName.Split(' ')[0];
            _currentCharacter.text = firstWord;
            Action onFinishMove = () => GameController.Instance.CombatModel.FinishMove();

            ActivateButtons(_selectedCharacter, onFinishMove);
        }

        SquadController tempAISquad;
        SquadController tempPlayerSquad;

        private void ActivateButtons(CharacterController selectedCharacter, Action onFinishMove)
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
                        tempAISquad.Characters,
                        onFinishMove);
                }
                if (skill is CharacterRangeSkill)
                {
                    AddActionToButton(_attackMeleButton,
                        selectedCharacter,
                        skill,
                        tempAISquad.Characters,
                        onFinishMove);
                }
                if (skill is CharacterBufSkill)
                {
                    AddActionToButton(_bufButton,
                        selectedCharacter,
                        skill,
                        tempPlayerSquad.Characters,
                        onFinishMove);
                }
                if (skill is CharacterDebufSkill)
                {
                    AddActionToButton(_debuffButton,
                        selectedCharacter,
                        skill,
                        tempAISquad.Characters,
                        onFinishMove);
                }
                if (skill is CharacterHealSkill)
                {
                    AddActionToButton(_healButton,
                        selectedCharacter,
                        skill,
                        tempPlayerSquad.Characters,
                        onFinishMove);
                }
                if (skill is CharacterAreaDamageSkill)
                {
                    AddActionToButton(_attackAreaButton,
                        selectedCharacter,
                        skill,
                        tempAISquad.Characters,
                        onFinishMove);
                }
            }
        }

        //principle DRY (Don't Repeat Yourself)
        private void AddActionToButton(Button button,
            CharacterController selectedCharacter,
            CharacterSkill skill,
            List<CharacterController> characterTargets,
            Action onFinishMove)
        {
            button.gameObject.SetActive(true);

            button.onClick.RemoveAllListeners();

            button.onClick.AddListener(() =>
            {
                skill.UseSkill(
                    selectedCharacter,
                    characterTargets,
                    onFinishMove);

                DeactivateAllButton();
                HighlightCharacter(selectedCharacter, false);
            });
        }

        private void HighlightCharacter(CharacterController character, bool enableHighlight)
        {
            string fullName = character.name.ToString();
            string firstWord = fullName.Split(' ')[0];

            // Find the child object that matches the first word of the character's name
            Transform targetTransform = character.transform.Find(firstWord);
            Debug.Log("\t" + targetTransform);
            if (targetTransform != null)
            {
                Renderer characterRenderer = targetTransform.GetComponentInChildren<Renderer>();
                Debug.Log("\t" + "f\t" + characterRenderer);

                if (characterRenderer != null)
                {
                    if (enableHighlight)
                    {
                        characterRenderer.material.DOColor(Color.green, 0.5f) // Light gray
                            .OnComplete(() =>
                            {
                                characterRenderer.material.DOColor(new Color(1f, 1f, 1f, 0.5f), 0.5f) // Semi-transparent white
                                    .SetLoops(-1, LoopType.Yoyo);
                            });
                    }
                    else
                    {
                        characterRenderer.material.DOKill();
                        characterRenderer.material.DOColor(Color.white, 0.5f);
                    }
                }
            }
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

