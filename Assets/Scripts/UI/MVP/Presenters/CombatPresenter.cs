using System;
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
            HighlightCharacter(_selectedCharacter, true);
            string fullName = _selectedCharacter.name.ToString();
            string firstWord = fullName.Split(' ')[0];
            _currentCharacter.text = firstWord;
            Action onFinishMove = ()=> GameController.Instance.CombatModel.FinishMove();

            ActivateButtons(_selectedCharacter, onFinishMove);
        }

        SquadController tempAISquad;
        SquadController tempPlayerSquad;

        private void ActivateButtons(CharacterController _selectedCharacter, Action onFinishMove)
        {
            
            SquadController parent = _selectedCharacter.GetComponentInParent<SquadController>();
            if(parent.name == "SquadPlayerController"){
                tempAISquad = GameController.Instance.CombatModel.AiSquad;
                tempPlayerSquad = GameController.Instance.CombatModel.PlayerSquad;
            }else if (parent.name == "SquadAIController")
            {
                tempAISquad = GameController.Instance.CombatModel.PlayerSquad;
                tempPlayerSquad = GameController.Instance.CombatModel.AiSquad;
            }

            
            for (int i = 0; i < _selectedCharacter.Skills.Length; i++)
            {
                

                var skill = _selectedCharacter.Skills[i];

                if (skill is CharacterMeleSkill)
                {
                    _attackMeleButton.gameObject.SetActive(true);
                    _attackMeleButton.onClick.AddListener(() =>
                    {
                        skill.UseSkill(
                            _selectedCharacter,
                            tempAISquad.Characters,
                            onFinishMove);

                        DeactivateAllButton();
                        HighlightCharacter(_selectedCharacter, false);
                        Debug.LogWarning(_selectedCharacter.name + "\tя закончил ходить");
                    });

                }
                if (skill is CharacterRangeSkill)
                {
                    _attackRangeButton.gameObject.SetActive(true);
                    _attackRangeButton.onClick.AddListener(() =>
                    {

                        skill.UseSkill(
                            _selectedCharacter,
                            tempAISquad.Characters,
                            onFinishMove);

                        DeactivateAllButton();
                        HighlightCharacter(_selectedCharacter, false);
                        Debug.LogWarning(_selectedCharacter.name + "\tя закончил ходить");
                    });

                }
                if (skill is CharacterBufSkill)
                {
                    _bufButton.gameObject.SetActive(true);
                    _bufButton.onClick.AddListener(() =>
                    {
                        skill.UseSkill(
                            _selectedCharacter,
                            tempPlayerSquad.Characters,
                            onFinishMove);

                        DeactivateAllButton();
                        HighlightCharacter(_selectedCharacter, false);
                        Debug.LogWarning(_selectedCharacter.name + "\tя закончил ходить");
                    });

                }
                if (skill is CharacterDebufSkill)
                {
                    _debuffButton.gameObject.SetActive(true);
                    _debuffButton.onClick.AddListener(() =>
                    {

                        skill.UseSkill(
                            _selectedCharacter,
                            tempAISquad.Characters,
                            onFinishMove);

                        DeactivateAllButton();
                        HighlightCharacter(_selectedCharacter, false);
                        Debug.LogWarning(_selectedCharacter.name + "\tя закончил ходить");
                    });

                }
                if (skill is CharacterHealSkill)
                {
                    _healButton.gameObject.SetActive(true);
                    _healButton.onClick.AddListener(() =>
                    {
                        skill.UseSkill(
                            _selectedCharacter,
                            tempPlayerSquad.Characters,
                            onFinishMove);

                        DeactivateAllButton();
                        HighlightCharacter(_selectedCharacter, false);
                        Debug.LogWarning(_selectedCharacter.name + "\tя закончил ходить");
                    });

                }
                if (skill is CharacterAreaDamageSkill)
                {
                    _attackAreaButton.gameObject.SetActive(true);
                    _attackAreaButton.onClick.AddListener(() =>
                    {
                        skill.UseSkill(
                            _selectedCharacter,
                            tempAISquad.Characters,
                            onFinishMove);

                        DeactivateAllButton();
                        HighlightCharacter(_selectedCharacter, false);
                        Debug.LogWarning(_selectedCharacter.name + "\tя закончил ходить");
                    });

                }
            }
        }

        private void HighlightCharacter(CharacterController character, bool enableHighlight)
        {
            Renderer characterRenderer = character.GetComponentInChildren<Renderer>();
            
            if (characterRenderer != null)
            {
                if (enableHighlight)
                {
                    Debug.Log(character.name + " \tсвет");
                    characterRenderer.material.DOColor(Color.green, 0.5f)
                        .OnComplete(() =>
                        {
                            characterRenderer.material.DOColor(new Color(1f, 1f, 1f, 0.5f), 0.5f)
                                .SetLoops(-1, LoopType.Yoyo);
                        });
                }
                else
                {
                    Debug.Log(character.name + " \tстоп");
                    characterRenderer.material.DOKill();
                    characterRenderer.material.DOColor(Color.white, 0.5f);
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

