using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController = AFG.Character.CharacterController;

namespace AFG.Character
{
    [System.Serializable]
    public class CharacterBrain
    {
        protected CharacterBrainType _brainType;
        protected CharacterController _characterController;
        
        public virtual void Initialization(CharacterController characterController)
        {
            _characterController = characterController;
        }
    }
}
