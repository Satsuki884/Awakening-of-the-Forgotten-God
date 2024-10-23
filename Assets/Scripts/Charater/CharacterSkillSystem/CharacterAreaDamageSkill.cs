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
    public class CharacterAreaDamageSkill : CharacterSkill
    {

        private float _atk;
        //private List<CharacterController> _areaTargets;
        public override void UseSkill(CharacterController user,
            List<CharacterController> targets, Action OnSkillUsed)
        {
            base.UseSkill(user, targets, OnSkillUsed);

            _atk = user.Atk;
            //_areaTargets = targets;

            for (int i = 0; i < targets.Count; i++)
            {
                int j = i;
                targets[j].IsAbleToSelect = true;

                targets[j].OnSelected += OnCharacterSelected;
            }

            Debug.Log("Area skill used");
        }

        public override void OnCharacterSelected(CharacterController characterController)
        {
            base.OnCharacterSelected(characterController);

            //play run animation
            _user.AnimationController.PlayRunAnimation();

            Vector3 startPoint = _user.transform.position;

            //run to enemy
            _user.MoveController.MoveTo(characterController.transform.position, () =>
            {
                //start hit enemy
                _user.AnimationController.PlayAreaAnimation(() =>
                {
                    Debug.LogWarning("Start area dmdge");
                    //enemy hit
                    for (int i = 0; i < _targets.Count; i++)
                    {
                           
                        //enemy hit
                        _targets[i].DamageController.TakeDamage(_atk, _targets[i]);
                            //return to start point
                            _user.MoveController.MoveTo(startPoint, () =>
                            {
                                //play idle animation on start point
                                _user.AnimationController.PlayIdleAnimation();
                                onSkillUsed?.Invoke();
                            });
                    }
                    Debug.LogWarning("Range skill used on all enemy");
                });
            });
            
            DeactivateSelectionAbility(_targets);
        }


       
    }
}

