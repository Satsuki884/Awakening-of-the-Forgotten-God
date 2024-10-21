using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    //TODO add realization
    public void PlayRunAnimation()
    {
        Debug.Log("Play run animation");
    }

    //TODO add realization use animation events
    public void PlayMeleAttackAnimation(Action OnHit)
    {
        Debug.Log("Play mele animation");
        OnHit?.Invoke();
    }

    public void PlayIdleAnimation()
    {
        Debug.Log("Play idle animation");
    }
}
