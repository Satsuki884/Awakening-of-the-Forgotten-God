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
        public override void UseSkill(CharacterController user,
            List<CharacterController> targets, Action OnSkillUsed)
        {
            base.UseSkill(user, targets, OnSkillUsed);

            //play run animation
            user.AnimationController.PlayRunAnimation();

            Vector3 startPoint = user.transform.position;

            Vector3 averagePosition = Vector3.zero;

            for (int i = 0; i < targets.Count; i++)
            {
                averagePosition += targets[i].transform.position;
            }
            averagePosition /= targets.Count;

            //run to enemy
            user.MoveController.MoveTo(averagePosition, () =>
            {
                for (int i = 0; i < targets.Count; i++)
                {
                    user.AnimationController.PlayAreaAnimation(() =>
                    {
                        //enemy hit
                        targets[i].DamageController.TakeDamage(user.Atk, targets[i]);
                        //play idle animation on start point
                        user.AnimationController.PlayIdleAnimation();
                        onSkillUsed?.Invoke();
                    });
                }
                
            });


            Debug.Log("Range skill used on all enemy");
            DeactivateSelectionAbility(_targets);
        }

        
    }
}

