using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFG.Character
{
    public class CharacterPlayerController : CharacterController
    {
        //TODO refactoring
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _moveController.MoveLeft();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _moveController.MoveRight();
            }
        }
    }
}
