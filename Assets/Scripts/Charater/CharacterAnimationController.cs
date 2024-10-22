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

    public void PlayRangeAttackAnimation(Action OnRange)
    {
        Debug.Log("Play range animation");
        OnRange?.Invoke();
    }

    public void PlayHealAnimation(Action OnHeal)
    {
        Debug.Log("Play heal animation");
        OnHeal?.Invoke();
    }

    public void PlayBufAnimation(Action OnBuf)
    {
        Debug.Log("Play buf animation");
        OnBuf?.Invoke();
    }

    public void PlayDebufAnimation(Action OnDebuf)
    {
        Debug.Log("Play debuf animation");
        OnDebuf?.Invoke();
    }

    public void PlayAreaAnimation(Action OnArea)
    {
        Debug.Log("Play area animation");
        OnArea?.Invoke();
    }

    public void PlayIdleAnimation()
    {
        Debug.Log("Play idle animation");
    }
}
