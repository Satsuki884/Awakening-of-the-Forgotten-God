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

        [SerializeField] private GameObject _rangeVfxPrefab;

        private ParticleSystem _vfx;

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

        protected override void OnTargetSelected(CharacterController characterController)
        {
            base.OnTargetSelected(characterController);

            Vector3 targetPosition = characterController.transform.position;

            //start hit enemy
            _user.AnimationController.PlayRangeAttackAnimation(_user, () =>
            {
                if (_vfx == null)
                {
                    _vfx = Instantiate(_rangeVfxPrefab, targetPosition, Quaternion.identity).GetComponent<ParticleSystem>();
                }

                _vfx.Play();
                //enemy hit
                characterController.DamageController.TakeDamage(_user.Atk, characterController);
                _vfx.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                //play idle animation on start point
                _user.AnimationController.PlayIdleAnimation(_user);
                onSkillUsed?.Invoke();
            });
        }
    }
}

