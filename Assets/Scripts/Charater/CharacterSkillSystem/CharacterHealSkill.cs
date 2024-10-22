using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFG.Character
{
    public class CharacterHealSkill : CharacterSkill
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

            Debug.Log("Heal skill used");
        }

        public override void OnCharacterSelected(CharacterController characterController)
        {
            base.OnCharacterSelected(characterController);
            //play run animation
            _user.AnimationController.PlayRunAnimation();

            Vector3 startPoint = _user.transform.position;

            _user.MoveController.MoveTo(characterController.transform.position, () =>
            {
                //start heal player character squad
                _user.AnimationController.PlayHealAnimation(() =>
                {
                    Debug.Log(characterController.HealController == true);
                    //heal
                    characterController.HealController.Healing(characterController, 10);

                    //return to start point
                    _user.MoveController.MoveTo(startPoint, () =>
                    {
                        //play idle animation on start point
                        _user.AnimationController.PlayIdleAnimation();
                        onSkillUsed?.Invoke();
                    });
                });
            });

            DeactivateSelectionAbility(_targets);
        }
    }
}
