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
    private string _idle = "idle";
    private string _death = "death";

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
            if(animator.GetBool(_death))
            {
                SetAllAnimationInFalse(animator, _death);
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
        if (_animator != null)
        {
            CheckSkill(character, _animator);
            _animator.SetBool(_run, true);
        }
    }

    public void PlayDeathAnimation(CharacterController character, Action OnDeath)
    {
        if (_animator != null)
        {
            CheckSkill(character, _animator);
            _animator.SetBool(_death, true);
            StartCoroutine(WaitForAnimation(GetAnimLong(_animator, _death), () =>
            {
                _animator.SetBool(_death, false);
                OnDeath?.Invoke();
            }));
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
            if (clip.name == type)
            {
                return clip.length;
            }
        }
        return 0f;
    }

    public void PlayRangeAttackAnimation(CharacterController character, Action OnRange)
    {
        if (_animator != null)
        {
            CheckSkill(character, _animator);
            _animator.SetBool(_range, true);
            StartCoroutine(WaitForAnimation(GetAnimLong(_animator, _range), () =>
            {
                _animator.SetBool(_range, false);
                OnRange?.Invoke();
            }));
        }
    }

    public void PlayHealAnimation(CharacterController character, Action OnHeal)
    {
        if (_animator != null)
        {
            CheckSkill(character, _animator);
            _animator.SetBool(_heal, true);
            StartCoroutine(WaitForAnimation(GetAnimLong(_animator, _heal), () =>
            {
                _animator.SetBool(_heal, false);
                OnHeal?.Invoke();
            }));
        }
    }

    public void PlayBufAnimation(CharacterController character, Action OnBuf)
    {
        if (_animator != null)
        {
            CheckSkill(character, _animator);
            _animator.SetBool(_buf, true);
            StartCoroutine(WaitForAnimation(GetAnimLong(_animator, _buf), () =>
            {
                _animator.SetBool(_buf, false);
                OnBuf?.Invoke();
            }));
        }
    }

    public void PlayDebufAnimation(CharacterController character, Action OnDebuf)
    {
        if (_animator != null)
        {
            CheckSkill(character, _animator);
            _animator.SetBool(_debuf, true);
            StartCoroutine(WaitForAnimation(GetAnimLong(_animator, _debuf), () =>
            {
                _animator.SetBool(_debuf, false);
                OnDebuf?.Invoke();
            }));
        }
    }

    public void PlayAreaAnimation(CharacterController character, Action OnArea)
    {
        if (_animator != null)
        {
            CheckSkill(character, _animator);
            _animator.SetBool(_area, true);
            StartCoroutine(WaitForAnimation(GetAnimLong(_animator, _area), () =>
            {
                _animator.SetBool(_area, false);
                OnArea?.Invoke();
            }));
        }
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