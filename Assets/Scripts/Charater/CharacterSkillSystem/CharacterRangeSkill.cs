using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using AFG.Character;
using AFG.Stats;
using DG.Tweening;


namespace AFG.Character
{
    public class CharacterRangeSkill : CharacterSkill
    {

        [SerializeField] private GameObject _rangeVfxPrefab;
        [SerializeField] private GameObject _hitVfxPrefab;

        private ParticleSystem _vfx;
        private ParticleSystem _hitVfx;

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

            //Debug.Log("Range skill used");
        }

        public override void UseAISkill(CharacterController user,
           CharacterController AITarget, Action OnSkillUsed)
        {
            base.UseAISkill(user, AITarget, OnSkillUsed);

            OnTargetSelected(AITarget);
        }

        Vector3 targetPosition;


        protected override void OnTargetSelected(CharacterController characterController)
        {
            base.OnTargetSelected(characterController);
            targetPosition = characterController.transform.position;

            //start hit enemy
            _user.AnimationController.PlayRangeAttackAnimation(_user, () =>
            {
                //ParticleMove(() =>
                //{
                    characterController.DamageController.TakeDamage(_user.Atk, characterController);
                    _user.AnimationController.PlayIdleAnimation(_user);
                    onSkillUsed?.Invoke();
                //});
            });
        }

        public void ParticleMove(/*Action OnMoveFinished*/)
        {
            Vector3 direction = (targetPosition - _user.transform.position).normalized;
            Vector3 adjustedPosition = targetPosition - direction * 3f;
            adjustedPosition.y += 1;
            targetPosition.y += 1;
            // if (_hitVfx == null)
            // {
                _hitVfx = Instantiate(_hitVfxPrefab, _user.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            // }
            _hitVfx.Play();

            _hitVfx.transform.DOMove(adjustedPosition, 1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                _hitVfx.transform.DOKill();
                _hitVfx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

                if (_vfx == null)
                {
                    _vfx = Instantiate(_rangeVfxPrefab, targetPosition, Quaternion.identity).GetComponent<ParticleSystem>();
                }
                _vfx.Play();
                //OnMoveFinished?.Invoke();
            });
        }
    }
}

