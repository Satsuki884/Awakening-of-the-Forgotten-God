using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

namespace AFG.Character
{
    public class CharacterDebufSkill : CharacterSkill
    {
        [SerializeField] private GameObject _debufVfxPrefab;

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

            //Debug.Log("Debuf skill used");
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

            //play run animation
            _user.AnimationController.PlayRunAnimation(_user);

            Vector3 startPoint = _user.transform.position;

            Quaternion initialRotation = _user.transform.rotation;

            targetPosition = characterController.transform.position;
            Vector3 direction = (targetPosition - _user.transform.position).normalized;
            Vector3 adjustedPosition = targetPosition - direction * 3f;


            _user.MoveController.MoveTo(_user, adjustedPosition, () =>
            {
                //start debuf enemy
                _user.AnimationController.PlayDebufAnimation(_user, () =>
                {

                    int randomBuf = UnityEngine.Random.Range(2, 4);
                    int randomBufValue = 1;
                    if (randomBuf == 2)
                    {
                        randomBufValue = UnityEngine.Random.Range(1, 5);
                    }
                    else if (randomBuf == 3)
                    {
                        randomBufValue = UnityEngine.Random.Range(1, 5);
                    }
                    //enemy debuf
                    characterController.DeBufController.TakeDeBuf(characterController, randomBuf, randomBufValue);
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

        public void ParticlePlay()
        {
            targetPosition.y += 1;

            if (_vfx == null)
            {
                _vfx = Instantiate(_debufVfxPrefab, targetPosition, Quaternion.identity).GetComponent<ParticleSystem>();
            }

            _vfx.Play();
        }
    }
}
