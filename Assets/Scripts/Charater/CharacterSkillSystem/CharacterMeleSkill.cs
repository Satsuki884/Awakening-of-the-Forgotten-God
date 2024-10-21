using System.Collections;
using System.Collections.Generic;
using AFG.Character;
using UnityEngine;

namespace AFG.Character
{
    public class CharacterMeleSkill : CharacterSkill
    {
        public override void UseSkill(CharacterController user, 
            List<CharacterController> targets)
        {
            base.UseSkill(user, targets);
            
            for(int i=0; i < targets.Count; i++)
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
            
            //play run animation
            _user.AnimationController.PlayRunAnimation();
            
            Vector3 startPoint = _user.transform.position;
            
            //run to enemy
            _user.MoveController.MoveTo(characterController.transform.position, () =>
            {
                //start hit enemy
                _user.AnimationController.PlayMeleAttackAnimation(() =>
                {
                    //enemy hit
                    characterController.DamageController.TakeDamage(10);
                    
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
