using System;
using System.Collections;
using System.Collections.Generic;
using AFG.Character;
using UnityEngine;

namespace AFG.Character
{
    public class CharacterMeleSkill : CharacterSkill
    {
        public override void UseSkill(CharacterController user,
            List<CharacterController> targets, Action OnSkillUsed)
        {
            base.UseSkill(user, targets, OnSkillUsed);

            Debug.LogWarning(user.name);

            for (int i=0; i < targets.Count; i++)
            {
                int j = i;
                targets[j].IsAbleToSelect = true;

                targets[j].OnSelected += OnCharacterSelected;
            }
            
            Debug.Log("Mele skill used");
        }
        
        public override void OnCharacterSelected(CharacterController characterController)
        {
            base.OnCharacterSelected(characterController);
            DeactivateSelectionAbility(_targets);

            //play run animation
            _user.AnimationController.PlayRunAnimation();
            
            Vector3 startPoint = _user.transform.position;
            Quaternion initialRotation = _user.transform.rotation;


            Vector3 targetPosition = characterController.transform.position;
            Vector3 direction = (targetPosition - _user.transform.position).normalized;
            Vector3 adjustedPosition = targetPosition - direction * 3f;


            _user.MoveController.MoveTo(_user, adjustedPosition, () =>
            {
                //start hit enemy
                _user.AnimationController.PlayMeleAttackAnimation(() =>
                {
                    //enemy hit
                    characterController.DamageController.TakeDamage(_user.Atk, characterController);
                    
                    //return to start point
                    _user.MoveController.MoveBack(_user, startPoint, initialRotation, () =>
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
