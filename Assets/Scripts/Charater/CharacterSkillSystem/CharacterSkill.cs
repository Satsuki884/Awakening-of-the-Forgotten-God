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
        protected CharacterController _aiTarget;

        protected Action onSkillUsed;

        public virtual void UseSkill(CharacterController user,
            List<CharacterController> targets,
            Action OnSkillUsed = null)
        {
            _user = user;
            _targets = targets;
            onSkillUsed = OnSkillUsed;
        }

        public virtual void UseAISkill(CharacterController user,
            CharacterController AITarget,
            Action OnSkillUsed = null)
        {
            _user = user;
            _aiTarget = AITarget;
            onSkillUsed = OnSkillUsed;
        }

        private void DeactivateSelectionAbility()
        {
            if (_targets != null)
            {
                for (int i = 0; i < _targets.Count; i++)
                {
                    _targets[i].IsAbleToSelect = false;
                    _targets[i].OnSelected -= OnTargetSelected;
                }
            }

        }

        protected virtual void OnTargetSelected(CharacterController characterController)
        {
            DeactivateSelectionAbility();
        }
    }
}
