using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using AFG.Character;
using AFG.Stats;


namespace AFG.Character
{
    public class CharacterRangeSkill : CharacterSkill
    {
        public override void UseSkill(CharacterController user,
            List<CharacterController> targets, Action OnSkillUsed)
        {
            base.UseSkill(user, targets, OnSkillUsed);

            for (int i = 0; i < targets.Count; i++)
            {
                int j = i;
                targets[j].IsAbleToSelect = true;

                targets[j].OnSelected += OnCharacterSelected;
            }

            //Debug.Log("Range skill used");
        }

        public override void OnCharacterSelected(CharacterController characterController)
        {
            base.OnCharacterSelected(characterController);

            //start hit enemy
            _user.AnimationController.PlayRangeAttackAnimation(_user, () =>
            {
                //enemy hit
                characterController.DamageController.TakeDamage(_user.Atk, characterController);
                //play idle animation on start point
                _user.AnimationController.PlayIdleAnimation(_user);
                onSkillUsed?.Invoke();
            });

            DeactivateSelectionAbility(_targets);
        }
    }
}

