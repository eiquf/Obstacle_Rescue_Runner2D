using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class CharacterAnimation
{
    public Action<bool> IsJump;
    public Action<bool> IsStop;
    public Action IsInjured;
    public enum Keys { Jump, Idle, Run, Injure }

    [Header("Injury Effect Settings")]
    private Color originalColor = new(1, 1, 1);
    private SpriteRenderer _spriteRenderer;
    public float flashDuration = 0.1f;
    public int flashCount = 3;

    private Animator _animator;

    private readonly Dictionary<Keys, string> keys = new() { { Keys.Jump, "Jump" }, { Keys.Idle, "Idle" } };
    public void Inject(Animator animator, SpriteRenderer spriteRenderer)
    {
        _animator = animator;
        _spriteRenderer = spriteRenderer;

        IsStop += Idle;
        IsJump += Jump;
        IsInjured += Injure;
    }
    public void Dispose()
    {
        IsStop -= Idle;
        IsJump -= Jump;
        IsInjured -= Injure;
    }
    private void Jump(bool state) => SetAnimationState(keys[Keys.Jump], state);
    private void Idle(bool state) => SetAnimationState(keys[Keys.Idle], state);
    private void Injure()
    {
        Sequence injurySequence = DOTween.Sequence();

        for (int i = 0; i < flashCount; i++)
        {
            injurySequence.Append(_spriteRenderer.DOColor(Color.red, flashDuration / 2));
            injurySequence.Append(_spriteRenderer.DOColor(originalColor, flashDuration / 2));
        }

        injurySequence.Play();
    }
    private void SetAnimationState(string animation, bool activate) => _animator.SetBool(animation, activate);
}