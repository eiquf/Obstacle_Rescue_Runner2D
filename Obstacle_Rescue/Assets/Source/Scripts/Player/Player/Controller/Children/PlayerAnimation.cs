using System;
using UnityEngine;

public class PlayerAnimation : IDisposable
{
    public Action PlayerJumpAnimation { get; private set; }
    public Action PlayerIsStop;
    public Action PlayerIsUpset;
    public Action PlayerGameStartedAnimation;

    private readonly Animator _playerAnimator;

    public PlayerAnimation(Animator animator)
    {
        _playerAnimator = animator;

        PlayerIsStop += PlayerIdle;
        PlayerJumpAnimation += PlayerJump;
        PlayerIsUpset += PlayerLose;
    }
    private void PlayerJump() => SetAnimationState("Jump", true);
    private void PlayerIdle() => SetAnimationState("Idle", true);
    private void PlayerLose() => SetAnimationState("GameOver", true);
    private void SetAnimationState(string animation, bool activate)
    {
        if (_playerAnimator != null) return;

        _playerAnimator.SetBool(animation, activate);
    }
    public void Dispose()
    {
        PlayerIsStop -= PlayerIdle;
        PlayerJumpAnimation -= PlayerJump;
        PlayerIsUpset -= PlayerLose;

        Debug.Log("Hiiiiiiiiiii");
    }
}
