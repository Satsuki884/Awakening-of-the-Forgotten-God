using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFG.Character
{
    public class CharacterSkill : MonoBehaviour
    {
        protected CharacterController _user;
        protected List<CharacterController> _targets;
        
        protected Action onSkillUsed;
        
        public virtual void UseSkill(CharacterController user, 
            List<CharacterController> targets,
            Action OnSkillUsed = null)
        {
            _user = user;
            _targets = targets;
            onSkillUsed = OnSkillUsed;
        }

        private void DeactivateSelectionAbility()
        {
            for(int i=0; i < _targets.Count; i++)
            {
                _targets[i].IsAbleToSelect = false;
                _targets[i].OnSelected -= OnTargetSelected;
            }
        }
        
        protected virtual void OnTargetSelected(CharacterController characterController)
        {
            DeactivateSelectionAbility();
        }
    }
}
