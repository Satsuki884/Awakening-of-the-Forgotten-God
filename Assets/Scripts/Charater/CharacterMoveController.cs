using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CharacterController = AFG.Character.CharacterController;

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

    public void MoveTo(CharacterController user, Vector3 transformPosition, Action OnMoveFinished)
    {
        Vector3 direction = (transformPosition - user.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        user.transform.DORotateQuaternion(targetRotation, 1f).OnComplete(() =>
        {
            user.transform.DOMove(transformPosition, 1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                OnMoveFinished?.Invoke();
            });
        });
    }

    public void MoveBack(CharacterController user, Vector3 transformPosition, Quaternion transformRotation, Action OnMoveFinished)
    {
        Vector3 direction = (transformPosition - user.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        user.transform.DORotateQuaternion(targetRotation, 1f).OnComplete(() =>
        {
            user.transform.DOMove(transformPosition, 1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                user.transform.DORotateQuaternion(transformRotation, 1f).OnComplete(() =>
                {
                    OnMoveFinished?.Invoke();
                });
            });
        });
    }
}
