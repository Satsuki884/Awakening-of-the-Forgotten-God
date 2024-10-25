using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

namespace AFG.Character
{
    public class CharacterDebufSkill : CharacterSkill
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

            //Debug.Log("Debuf skill used");
        }

        public override void OnCharacterSelected(CharacterController characterController)
        {
            base.OnCharacterSelected(characterController);
            DeactivateSelectionAbility(_targets);
            //play run animation
            _user.AnimationController.PlayRunAnimation(_user);

            Vector3 startPoint = _user.transform.position;

            Quaternion initialRotation = _user.transform.rotation;

            Vector3 targetPosition = characterController.transform.position;
            Vector3 direction = (targetPosition - _user.transform.position).normalized;
            Vector3 adjustedPosition = targetPosition - direction * 3f;


            _user.MoveController.MoveTo(_user, adjustedPosition, () =>
            {
                //start debuf enemy
                _user.AnimationController.PlayDebufAnimation(_user ,() =>
                {
                    //enemy debuf
                    characterController.DeBufController.TakeDeBuf(characterController, 2, 2);
                    _user.AnimationController.PlayRunAnimation(_user);

                    //return to start point
                    _user.MoveController.MoveBack(_user, startPoint, initialRotation, () =>
                    {
                        //play idle animation on start point
                        _user.AnimationController.PlayIdleAnimation(_user);
                        onSkillUsed?.Invoke();
                    });
                });
            });

            DeactivateSelectionAbility(_targets);
        }
    }
}
