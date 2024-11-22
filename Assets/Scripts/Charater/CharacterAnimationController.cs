using AFG.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController = AFG.Character.CharacterController;

public class CharacterAnimationController : MonoBehaviour
{

    private Animator childAnimator;

    public void CheckSkill(CharacterController character, Animator animator)
    {
        //var skill = character.Skills;
        for (int i = 0; i < character.Skills.Length; i++)
        {
            var skill = character.Skills[i];
            if (skill is CharacterMeleSkill)
            {
                SetAllAnimationInFalse(animator, "melee");
            }
            if (skill is CharacterRangeSkill)
            {
                SetAllAnimationInFalse(animator, "range");
            }
            if (skill is CharacterBufSkill)
            {
                SetAllAnimationInFalse(animator, "buf");
            }
            if (skill is CharacterDebufSkill)
            {
                SetAllAnimationInFalse(animator, "debuf");
            }
            if (skill is CharacterHealSkill)
            {
                SetAllAnimationInFalse(animator, "heal");

            }
            if (skill is CharacterAreaDamageSkill)
            {
                SetAllAnimationInFalse(animator, "area");

            }
        }
    }

    public void SetAllAnimationInFalse(Animator animator, string type)
    {
        animator.SetBool(type, false);
        animator.SetBool("run", false);
    }

    public void PlayRunAnimation(CharacterController character)
    {
        //Debug.Log("Play run animation");

        childAnimator = character.GetComponentInChildren<Animator>();
        if (childAnimator != null)
        {
            CheckSkill(character, childAnimator);
            childAnimator.SetBool("run", true);
        }
    }

    public void PlayMeleeAttackAnimation(CharacterController character, Action OnHit)
    {
        //Debug.Log("Play mele animation");

        childAnimator = character.GetComponentInChildren<Animator>();
        if (childAnimator != null)
        {
            CheckSkill(character, childAnimator);
            childAnimator.SetBool("melee", true);
        }

        OnHit?.Invoke();
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

        childAnimator = character.GetComponentInChildren<Animator>();
        if (childAnimator != null)
        {
            CheckSkill(character, childAnimator);
            childAnimator.SetBool("range", true);
            StartCoroutine(WaitForAnimation(GetAnimLong(childAnimator, "range"), () =>
            {
                childAnimator.SetBool("range", false); // ������������� �������� ����� ����������
                OnRange?.Invoke();
            }));
        }

        //OnRange?.Invoke();
    }

    public void PlayHealAnimation(CharacterController character, Action OnHeal)
    {
        //Debug.Log("Play heal animation");

        childAnimator = character.GetComponentInChildren<Animator>();
        if (childAnimator != null)
        {
            CheckSkill(character, childAnimator);
            childAnimator.SetBool("heal", true);
            StartCoroutine(WaitForAnimation(GetAnimLong(childAnimator, "heal"), () =>
            {
                childAnimator.SetBool("heal", false); // ������������� �������� ����� ����������
                OnHeal?.Invoke();
            }));
        }

        OnHeal?.Invoke();
    }

    public void PlayBufAnimation(CharacterController character, Action OnBuf)
    {
        //Debug.Log("Play buf animation");

        childAnimator = character.GetComponentInChildren<Animator>();
        if (childAnimator != null)
        {
            CheckSkill(character, childAnimator);
            childAnimator.SetBool("buf", true);
            StartCoroutine(WaitForAnimation(GetAnimLong(childAnimator, "buf"), () =>
            {
                childAnimator.SetBool("buf", false); // ������������� �������� ����� ����������
                OnBuf?.Invoke();
            }));
        }

        OnBuf?.Invoke();
    }

    public void PlayDebufAnimation(CharacterController character, Action OnDebuf)
    {
        //Debug.Log("Play debuf animation");

        childAnimator = character.GetComponentInChildren<Animator>();
        if (childAnimator != null)
        {
            CheckSkill(character, childAnimator);
            childAnimator.SetBool("debuf", true);
            StartCoroutine(WaitForAnimation(GetAnimLong(childAnimator, "debuf"), () =>
            {
                childAnimator.SetBool("debuf", false); // ������������� �������� ����� ����������
                OnDebuf?.Invoke();
            }));
        }

        OnDebuf?.Invoke();
    }

    public void PlayAreaAnimation(CharacterController character, Action OnArea)
    {
        //Debug.Log("Play area animation");

        childAnimator = character.GetComponentInChildren<Animator>();
        if (childAnimator != null)
        {
            CheckSkill(character, childAnimator);
            childAnimator.SetBool("area", true);
            StartCoroutine(WaitForAnimation(GetAnimLong(childAnimator, "area"), () =>
            {
                childAnimator.SetBool("area", false); // ������������� �������� ����� ����������
                OnArea?.Invoke();
            }));
        }

        OnArea?.Invoke();
    }

    public void PlayIdleAnimation(CharacterController character)
    {

        childAnimator = character.GetComponentInChildren<Animator>();
        if (childAnimator != null)
        {
            CheckSkill(character, childAnimator);
        }
        //Debug.Log("Play idle animation");
    }
}