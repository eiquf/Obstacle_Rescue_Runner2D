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
    public float flashDuration = 0.1f;
    public int flashCount = 3;

    private readonly Sequence _injurySequence = DOTween.Sequence();
    private readonly SpriteRenderer _spriteRenderer;
    private readonly Animator _animator;

    private readonly Dictionary<Keys, string> keys = new()
    {
        { Keys.Jump, "Jump" },
        { Keys.Idle, "Idle" }
    };

    private readonly Dictionary<Keys, Action<bool>> _stateEventHandlers = new();

    public CharacterAnimation(Animator animator, SpriteRenderer spriteRenderer)
    {
        _animator = animator;
        _spriteRenderer = spriteRenderer;
    }

    public void Initialize()
    {
        _stateEventHandlers[Keys.Jump] = Jump;
        _stateEventHandlers[Keys.Idle] = Idle;

        IsStop += _stateEventHandlers[Keys.Idle];
        IsJump += _stateEventHandlers[Keys.Jump];
        IsInjured += Injure;
    }

    public void Dispose()
    {
        foreach (var handler in _stateEventHandlers.Values)
        {
            IsStop -= handler;
            IsJump -= handler;
        }
        IsInjured -= Injure;
        _injurySequence.Kill();   
    }

    private void Jump(bool state) => SetAnimationState(keys[Keys.Jump], state);
    private void Idle(bool state) => SetAnimationState(keys[Keys.Idle], state);
    private void Injure()
    {
        for (int i = 0; i < flashCount; i++)
        {
            _injurySequence.Append(_spriteRenderer.DOColor(Color.red, flashDuration / 2));
            _injurySequence.Append(_spriteRenderer.DOColor(originalColor, flashDuration / 2));
        }

        _injurySequence.Play();
    }

    private void SetAnimationState(string animation, bool activate) => _animator.SetBool(animation, activate);
}