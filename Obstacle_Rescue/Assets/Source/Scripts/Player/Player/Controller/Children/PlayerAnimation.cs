using System;
using UnityEngine;

public class PlayerAnimation : IDisposable
{
    public Action<bool> PlayerJumpAnimation;
    public Action<bool> PlayerIsStop;
    public Action PlayerIsUpset;

    private readonly Animator _playerAnimator;

    public PlayerAnimation(Animator animator)
    {
        PlayerIsStop += PlayerIdle;
        PlayerJumpAnimation += PlayerJump;
        PlayerIsUpset += PlayerLose;

        _playerAnimator = animator;
    }
    private void PlayerJump(bool state) => SetAnimationState("Jump", state);
    private void PlayerIdle(bool state) => SetAnimationState("Idle", state);
    private void PlayerLose() => SetAnimationState("GameOver", true);
    private void SetAnimationState(string animation, bool activate)
    {
        if (_playerAnimator == null) return;
        else _playerAnimator.SetBool(animation, activate);
    }
    public void Dispose()
    {
        PlayerIsStop -= PlayerIdle;
        PlayerJumpAnimation -= PlayerJump;
        PlayerIsUpset -= PlayerLose;
    }
}
