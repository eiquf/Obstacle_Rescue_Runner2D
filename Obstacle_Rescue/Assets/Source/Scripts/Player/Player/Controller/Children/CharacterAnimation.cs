using System;
using UnityEngine;
using Zenject;

public class CharacterAnimation : IDisposable, IInitializable
{
    public Action<bool> IsJump;
    public Action<bool> IsStop;

    private Animator _animator;

    private const string JumpAnimation = "Jump";
    private const string IdleAnimation = "Idle";

    public void Inject(Animator animator) => _animator = animator;
    public void Dispose()
    {
        IsStop -= Idle;
        IsJump -= Jump;
    }

    public void Initialize()
    {
        IsStop += Idle;
        IsJump += Jump;
    }

    private void Jump(bool state) => SetAnimationState(JumpAnimation, state);
    private void Idle(bool state) => SetAnimationState(IdleAnimation, state);

    private void SetAnimationState(string animation, bool activate)
    {
        if (_animator == null)
        {
            Debug.LogWarning("Animator is not set in CharacterAnimation.");
            return;
        }

        _animator.SetBool(animation, activate);
    }

}