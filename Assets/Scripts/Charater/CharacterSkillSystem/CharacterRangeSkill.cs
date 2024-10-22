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
        private float Atk;
        public override void UseSkill(CharacterController user,
            List<CharacterController> targets, Action OnSkillUsed)
        {
            base.UseSkill(user, targets, OnSkillUsed);

            Atk = user.Atk;

            for (int i = 0; i < targets.Count; i++)
            {
                int j = i;
                targets[j].IsAbleToSelect = true;

                targets[j].OnSelected += OnCharacterSelected;
            }

            Debug.Log("Range skill used");
        }

        public override void OnCharacterSelected(CharacterController characterController)
        {
            base.OnCharacterSelected(characterController);

            //start hit enemy
            _user.AnimationController.PlayRangeAttackAnimation(() =>
            {
                //enemy hit
                characterController.DamageController.TakeDamage(Atk, characterController);
                //play idle animation on start point
                _user.AnimationController.PlayIdleAnimation();
                onSkillUsed?.Invoke();
            });

            DeactivateSelectionAbility(_targets);
        }
    }
}

