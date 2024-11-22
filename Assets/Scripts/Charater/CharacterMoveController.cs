using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CharacterController = AFG.Character.CharacterController;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class CharacterMoveController : MonoBehaviour
{
    public void MoveTo(CharacterController user, Vector3 transformPosition, Action OnMoveFinished)
    {
        //user.AnimationController.PlayRunAnimation(user);
        //Debug.LogWarning(user.name);
        Vector3 direction = (transformPosition - user.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        user.transform.DOKill();

        user.transform.DORotateQuaternion(targetRotation, 0.1f).OnComplete(() =>
        {
            user.transform.DOKill();
            user.transform.DOMove(transformPosition, 1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                user.transform.DOKill();
                OnMoveFinished?.Invoke();
            });
        });
    }

    public void MoveBack(CharacterController user, Vector3 transformPosition, Quaternion transformRotation, Action OnMoveFinished)
    {
        //user.AnimationController.PlayRunAnimation(user);
        Vector3 direction = (transformPosition - user.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        user.transform.DOKill();

        user.transform.DORotateQuaternion(targetRotation, 0.1f).OnComplete(() =>
        {
            user.transform.DOKill();
            user.transform.DOMove(transformPosition, 1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                user.transform.DOKill();
                user.transform.DORotateQuaternion(transformRotation, 0.5f).OnComplete(() =>
                {
                    user.transform.DOKill();
                    OnMoveFinished?.Invoke();
                });
            });
        });
    }

}
