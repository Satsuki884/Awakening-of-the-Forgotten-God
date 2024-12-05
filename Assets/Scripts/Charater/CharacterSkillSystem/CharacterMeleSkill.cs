using System;
using System.Collections;
using System.Collections.Generic;
using AFG.Character;
using UnityEngine;
using UnityEngine.Serialization;

namespace AFG.Character
{
    public class CharacterMeleSkill : CharacterSkill
    {
        [SerializeField] private GameObject _meleeVfxPrefab;

        private ParticleSystem _vfx;
        
        public override void UseSkill(CharacterController user,
            List<CharacterController> targets, Action OnSkillUsed)
        {
            base.UseSkill(user, targets, OnSkillUsed);

            //Debug.LogWarning(user.name);

            for (int i=0; i < targets.Count; i++)
            {
                int j = i;
                targets[j].IsAbleToSelect = true;
                
                targets[j].OnSelected -= OnTargetSelected;
                targets[j].OnSelected += OnTargetSelected;
            }
            
            //Debug.Log("Mele skill used");
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
                _user.AnimationController.PlayMeleeAttackAnimation(_user ,() =>
                {
                    //enemy hit
                    Debug.Log("Enemy hit");
                    
                    //TODO add pool system
                    if (_vfx == null)
                    {
                        _vfx = Instantiate(_meleeVfxPrefab, targetPosition, Quaternion.identity).GetComponent<ParticleSystem>();
                    }
                    
                    _vfx.Play();
                    
                    characterController.DamageController.TakeDamage(_user.Atk, characterController);
                    _user.AnimationController.PlayRunAnimation(_user);

                    //return to start point
                    _user.MoveController.MoveBack(_user, startPoint, initialRotation, () =>
                    {
                        // _vfx.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                        //play idle animation on start point
                        _user.AnimationController.PlayIdleAnimation(_user);
                        onSkillUsed?.Invoke();
                    });
                });
            });
        }
    }
}
