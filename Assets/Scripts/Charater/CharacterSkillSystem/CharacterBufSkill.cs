using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFG.Character
{
    public class CharacterBufSkill : CharacterSkill
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

            Debug.Log("Buf skill used");
        }

        public override void OnCharacterSelected(CharacterController characterController)
        {
            base.OnCharacterSelected(characterController);

            //play run animation
            _user.AnimationController.PlayRunAnimation();

            Vector3 startPoint = _user.transform.position;

            _user.MoveController.MoveTo(characterController.transform.position, () =>
            {
                //start buf 
                _user.AnimationController.PlayBufAnimation(() =>
                {
                    //enemy hit
                    characterController.DeBufController.TakeBuf(characterController, 2, 5);

                    //return to start point
                    _user.MoveController.MoveTo(startPoint, () =>
                    {
                        //play idle animation on start point
                        _user.AnimationController.PlayIdleAnimation();
                    });
                });
            });

            DeactivateSelectionAbility(_targets);
        }
    }
}
