using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    //TODO add realization
    public void MoveLeft()
    {
       Debug.Log("Move left");
    }

    public void MoveRight()
    {
        Debug.Log("Move right");
    }

    public void MoveTo(Vector3 transformPosition, Action OnMoveFinished)
    {
        OnMoveFinished?.Invoke();
    }
}
