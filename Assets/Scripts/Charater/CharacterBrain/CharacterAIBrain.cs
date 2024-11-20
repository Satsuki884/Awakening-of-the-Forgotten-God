using System.Collections;
using System.Collections.Generic;
using AFG.Squad;
using UnityEngine;

namespace AFG.Character
{
    public class CharacterAIBrain : CharacterBrain
    {
        private SquadController _tempAISquad;
        private SquadController _tempPlayerSquad;
        
        public override void Initialization(CharacterController characterController)
        {
            base.Initialization(characterController);
            Type = CharacterBrainType.AI;
            
            GameController.Instance.CombatModel.OnCharacterSelected += OnCharacterSelected;
        }

        private void OnCharacterSelected(CharacterController selectedCharacter)
        {
            if (selectedCharacter != null && selectedCharacter.Brain.Type == CharacterBrainType.AI)
            {
                DoAiMove(selectedCharacter);
            }
        }
        
        private void DoAiMove(CharacterController selectedCharacter)
        {
            SquadController parent = selectedCharacter.GetComponentInParent<SquadController>();

            //TODO implement brain check
            //TODO "SquadPlayerController" move to variable
            if (parent.name == "SquadPlayerController")
            {
                _tempAISquad = GameController.Instance.CombatModel.AiSquad;
                _tempPlayerSquad = GameController.Instance.CombatModel.PlayerSquad;
            }
            else if (parent.name == "SquadAIController")
            {
                _tempAISquad = GameController.Instance.CombatModel.PlayerSquad;
                _tempPlayerSquad = GameController.Instance.CombatModel.AiSquad;
            }

            //AI select random skill
            int randSkillIndex = Random.Range(0, selectedCharacter.Skills.Length);

            var skill = selectedCharacter.Skills[randSkillIndex];

            if (skill is CharacterMeleSkill)
            {
                UseSkill(selectedCharacter,
                    skill,
                    _tempAISquad.Characters);
            }
            else if (skill is CharacterRangeSkill)
            {
                UseSkill(selectedCharacter,
                    skill,
                    _tempAISquad.Characters);
            }
            else if (skill is CharacterBufSkill)
            {
                UseSkill(selectedCharacter,
                    skill,
                    _tempPlayerSquad.Characters);
            }
            else if (skill is CharacterDebufSkill)
            {
                UseSkill(selectedCharacter,
                    skill,
                    _tempAISquad.Characters);
            }
            else if (skill is CharacterHealSkill)
            {
                UseSkill(selectedCharacter,
                    skill,
                    _tempPlayerSquad.Characters);
            }
            else if (skill is CharacterAreaDamageSkill)
            {
                UseSkill(selectedCharacter,
                    skill,
                    _tempAISquad.Characters);
            }
        }

        //principle DRY (Don't Repeat Yourself)
        private void UseSkill(
            CharacterController selectedCharacter,
            CharacterSkill skill,
            List<CharacterController> characterTargets)
        {
            selectedCharacter.SelectedCharacterSkill = skill;
            GameController.Instance.CombatModel.SelectedTargets = characterTargets;
        }
    }
}

