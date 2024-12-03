using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using AFG.Character;
using AFG.Stats;
using AFG;


namespace AFG.Character
{
    public class CharacterAreaDamageSkill : CharacterSkill
    {

        public override void UseSkill(CharacterController user,
            List<CharacterController> targets, Action OnSkillUsed)
        {
            base.UseSkill(user, targets, OnSkillUsed);


            for (int i = 0; i < targets.Count; i++)
            {
                int j = i;
                targets[j].IsAbleToSelect = true;

                targets[j].OnSelected -= OnTargetSelected;
                targets[j].OnSelected += OnTargetSelected;
            }
        }

        public override void UseAISkill(CharacterController user,
           CharacterController AITarget, Action OnSkillUsed)
        {
            base.UseAISkill(user, AITarget, OnSkillUsed);

            OnTargetSelected(AITarget);
        }

        protected override void OnTargetSelected(CharacterController characterController)
        {
            base.OnTargetSelected(characterController);

            //play run animation
            _user.AnimationController.PlayRunAnimation(_user);

            Vector3 startPoint = _user.transform.position;
            Quaternion initialRotation = _user.transform.rotation;


            Vector3 targetPosition = characterController.transform.position;
            Vector3 direction = (targetPosition - _user.transform.position).normalized;
            Vector3 adjustedPosition = targetPosition - direction * 3f;

            _user.MoveController.MoveTo(_user, adjustedPosition, () =>
            {
                //start hit enemy
                _user.AnimationController.PlayAreaAnimation(_user, () =>
                {
                    //Debug.LogWarning("Start area dmdge");
                    //enemy hit
                    for (int i = 0; i < _targets.Count; i++)
                    {

                        //enemy hit
                        _targets[i].DamageController.TakeDamage(_user.Atk, _targets[i]);
                        //return to start point

                    }
                    _user.AnimationController.PlayRunAnimation(_user);
                    _user.MoveController.MoveBack(_user, startPoint, initialRotation, () =>
                    {
                        //play idle animation on start point
                        _user.AnimationController.PlayIdleAnimation(_user);
                        onSkillUsed?.Invoke();
                    });
                    //Debug.LogWarning("Range skill used on all enemy");
                });
            });
        }
    }
}

