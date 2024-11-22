using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFG.Character
{
    public class CharacterAIController : CharacterController
    {
        //TODO Implement AI logic
        private void Update()
        {
            bool isMoveLeft = UnityEngine.Random.Range(0, 2) == 0;
            // if (isMoveLeft)
            // {
            //     _moveController.MoveLeft();
            // }
            // else
            // {
            //     _moveController.MoveRight();
            // }
        }
    }
}
