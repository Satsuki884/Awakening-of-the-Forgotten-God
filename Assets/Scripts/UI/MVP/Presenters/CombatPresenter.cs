using System;
using AFG.Character;
using AFG.Squad;
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
            var _playerSquad = GameController.Instance.CombatModel.PlayerSquad;
            var _aiSquad = GameController.Instance.CombatModel.AiSquad;
            Action onFinishMove = ()=> GameController.Instance.CombatModel.FinishMove();

            ActivateButtons(_selectedCharacter, _aiSquad, _playerSquad, onFinishMove);
        }


        private void ActivateButtons(CharacterController _selectedCharacter, 
            Squad.SquadController _aiSquad, 
            Squad.SquadController _playerSquad, 
            Action onFinishMove)
        {
            for (int i = 0; i < _selectedCharacter.Skills.Length; i++)
            {

                var skill = _selectedCharacter.Skills[i];
                //Debug.LogWarning("skill: " + skill);
                //DeactivateAllButton();

                if (skill is CharacterMeleSkill)
                {
                    //Debug.Log("skill: " + skill);
                    _attackMeleButton.gameObject.SetActive(true);
                    _attackMeleButton.onClick.AddListener(() =>
                    {
                        skill.UseSkill(
                            _selectedCharacter,
                            _aiSquad.Characters,
                            onFinishMove);

                        DeactivateAllButton();
                    });

                }
                if (skill is CharacterRangeSkill)
                {
                    //Debug.Log("skill: " + skill);
                    _attackRangeButton.gameObject.SetActive(true);
                    //Debug.LogWarning("i worked too");
                    _attackRangeButton.onClick.AddListener(() =>
                    {

                        skill.UseSkill(
                            _selectedCharacter,
                            _aiSquad.Characters,
                            onFinishMove);

                        DeactivateAllButton();
                    });

                }
                if (skill is CharacterBufSkill)
                {
                    //Debug.Log("skill: " + skill);
                    _bufButton.gameObject.SetActive(true);
                    _bufButton.onClick.AddListener(() =>
                    {
                        skill.UseSkill(
                            _selectedCharacter,
                            _playerSquad.Characters,
                            onFinishMove);

                        DeactivateAllButton();
                    });

                }
                if (skill is CharacterDebufSkill)
                {
                    //Debug.Log("skill: " + skill);
                    _debuffButton.gameObject.SetActive(true);
                    //Debug.LogWarning("i worked");
                    _debuffButton.onClick.AddListener(() =>
                    {

                        skill.UseSkill(
                            _selectedCharacter,
                            _aiSquad.Characters,
                            onFinishMove);

                        DeactivateAllButton();
                    });

                }
                if (skill is CharacterHealSkill)
                {
                    //Debug.Log("skill: " + skill);
                    _healButton.gameObject.SetActive(true);
                    _healButton.onClick.AddListener(() =>
                    {
                        skill.UseSkill(
                            _selectedCharacter,
                            _playerSquad.Characters,
                            onFinishMove);

                        DeactivateAllButton();
                    });

                }
                if (skill is CharacterAreaDamageSkill)
                {
                    //Debug.Log("skill: " + skill);
                    _attackAreaButton.gameObject.SetActive(true);
                    _attackAreaButton.onClick.AddListener(() =>
                    {
                        skill.UseSkill(
                            _selectedCharacter,
                            _aiSquad.Characters,
                            onFinishMove);

                        DeactivateAllButton();
                    });

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

