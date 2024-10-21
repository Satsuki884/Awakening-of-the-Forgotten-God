using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFG.Character
{
    public class CharacterSkill : MonoBehaviour
    {
        protected CharacterController _user;
        protected List<CharacterController> _targets;
        public virtual void UseSkill(CharacterController user, 
            List<CharacterController> targets)
        {
            Debug.Log("Skill used by " + user.name);
            _user = user;
            _targets = targets;
        }

        public virtual void DeactivateSelectionAbility(List<CharacterController> targets)
        {
            for(int i=0; i < targets.Count; i++)
            {
                targets[i].IsAbleToSelect = false;
            }
        }
        
        public virtual void OnCharacterSelected(CharacterController characterController)
        {
            
        }
    }
}
