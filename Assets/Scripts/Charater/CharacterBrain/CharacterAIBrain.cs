using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFG.Character
{
    public class CharacterAIBrain : CharacterBrain
    {
        public override void Initialization(CharacterController characterController)
        {
            base.Initialization(characterController);
            _brainType = CharacterBrainType.AI;
        }
    }
}

