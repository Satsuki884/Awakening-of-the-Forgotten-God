using AFG.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController = AFG.Character.CharacterController;

public class CharacterAnimationController : MonoBehaviour
{
    private string _melee = "melee";
    private string _range = "range";
    private string _buf = "buf";
    private string _debuf = "debuf";
    private string _heal = "heal";
    private string _area = "area";
    private string _run = "run";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void CheckSkill(CharacterController character, Animator animator)
    {
        //var skill = character.Skills;
        for (int i = 0; i < character.Skills.Length; i++)
        {
            var skill = character.Skills[i];
            if (skill is CharacterMeleSkill)
            {
                SetAllAnimationInFalse(animator, _melee);
            }
            if (skill is CharacterRangeSkill)
            {
                SetAllAnimationInFalse(animator, _range);
            }
            if (skill is CharacterBufSkill)
            {
                SetAllAnimationInFalse(animator, _buf);
            }
            if (skill is CharacterDebufSkill)
            {
                SetAllAnimationInFalse(animator, _debuf);
            }
            if (skill is CharacterHealSkill)
            {
                SetAllAnimationInFalse(animator, _heal);

            }
            if (skill is CharacterAreaDamageSkill)
            {
                SetAllAnimationInFalse(animator, _area);

            }
        }
    }

    public void SetAllAnimationInFalse(Animator animator, string type)
    {
        animator.SetBool(type, false);
        animator.SetBool(_run, false);
    }

    public void PlayRunAnimation(CharacterController character)
    {
        //Debug.Log("Play run animation");

        
        if (_animator != null)
        {
            CheckSkill(character, _animator);
            _animator.SetBool(_run, true);
        }
    }

    public void PlayMeleeAttackAnimation(CharacterController character, Action OnHit)
    {
        if (_animator != null)
        {
             CheckSkill(character, _animator);
             _animator.SetBool(_melee, true);
             StartCoroutine(WaitForAnimation(GetAnimLong(_animator, _melee), () =>
             {
                 _animator.SetBool(_melee, false);
                 OnHit?.Invoke();
             }));
        }
    }

    private IEnumerator WaitForAnimation(float delay, Action onComplete)
    {
        yield return new WaitForSeconds(delay);
        onComplete?.Invoke();
    }

    public float GetAnimLong(Animator animator, string type)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if(clip.name == type)
            {
                return clip.length;
            }
        }
        return 0f;
    }

    public void PlayRangeAttackAnimation(CharacterController character, Action OnRange)
    {
        //Debug.Log("Play range animation");

        // childAnimator = character.GetComponentInChildren<Animator>();
        // if (childAnimator != null)
        // {
        //     CheckSkill(character, childAnimator);
        //     childAnimator.SetBool(_range, true);
        //     StartCoroutine(WaitForAnimation(GetAnimLong(childAnimator, _range), () =>
        //     {
        //         childAnimator.SetBool(_range, false);
        //         OnRange?.Invoke();
        //     }));
        // }

        OnRange?.Invoke();
    }

    public void PlayHealAnimation(CharacterController character, Action OnHeal)
    {
        //Debug.Log("Play heal animation");

        // childAnimator = character.GetComponentInChildren<Animator>();
        // if (childAnimator != null)
        // {
        //     CheckSkill(character, childAnimator);
        //     childAnimator.SetBool(_heal, true);
        //     StartCoroutine(WaitForAnimation(GetAnimLong(childAnimator, _heal), () =>
        //     {
        //         childAnimator.SetBool(_heal, false);
        //         OnHeal?.Invoke();
        //     }));
        // }

        OnHeal?.Invoke();
    }

    public void PlayBufAnimation(CharacterController character, Action OnBuf)
    {
        //Debug.Log("Play buf animation");

        // childAnimator = character.GetComponentInChildren<Animator>();
        // if (childAnimator != null)
        // {
        //     CheckSkill(character, childAnimator);
        //     childAnimator.SetBool(_buf, true);
        //     StartCoroutine(WaitForAnimation(GetAnimLong(childAnimator, _buf), () =>
        //     {
        //         childAnimator.SetBool(_buf, false); 
        //         OnBuf?.Invoke();
        //     }));
        // }

        OnBuf?.Invoke();
    }

    public void PlayDebufAnimation(CharacterController character, Action OnDebuf)
    {
        //Debug.Log("Play debuf animation");

        // childAnimator = character.GetComponentInChildren<Animator>();
        // if (childAnimator != null)
        // {
        //     CheckSkill(character, childAnimator);
        //     childAnimator.SetBool(_debuf, true);
        //     StartCoroutine(WaitForAnimation(GetAnimLong(childAnimator, _debuf), () =>
        //     {
        //         childAnimator.SetBool(_debuf, false);
        //         OnDebuf?.Invoke();
        //     }));
        // }

        OnDebuf?.Invoke();
    }

    public void PlayAreaAnimation(CharacterController character, Action OnArea)
    {
        //Debug.Log("Play area animation");

        // childAnimator = character.GetComponentInChildren<Animator>();
        // if (childAnimator != null)
        // {
        //     CheckSkill(character, childAnimator);
        //     childAnimator.SetBool(_area, true);
        //     StartCoroutine(WaitForAnimation(GetAnimLong(childAnimator, _area), () =>
        //     {
        //         childAnimator.SetBool(_area, false);
        //         OnArea?.Invoke();
        //     }));
        // }

        OnArea?.Invoke();
    }

    public void PlayIdleAnimation(CharacterController character)
    {

        _animator = character.GetComponentInChildren<Animator>();
        if (_animator != null)
        {
            CheckSkill(character, _animator);
        }
        //Debug.Log("Play idle animation");
    }
}