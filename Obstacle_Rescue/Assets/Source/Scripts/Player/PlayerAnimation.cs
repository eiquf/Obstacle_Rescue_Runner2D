using System;
using UnityEngine;

public class PlayerAnimation : IDisposable
{
    public Action<bool> PlayerJumpAnimation;
    public Action<bool> PlayerStoppedByTrap;
    public Action<bool> PlayerIsSit;
    public Action PlayerIsUpset;
    public Action PlayerGameStartedAnimation;

    private enum Animations
    {
        isTrap,
        isRun,
        isJump,
        isUpset,
        isSit
    }
    private string[] AnimationNames => new string[]
     {
        "isTrap",
        "isRun",
        "isJump",
        "isUpset",
        "isSit"
     };

    private readonly Animator _playerAnimator;

    public PlayerAnimation()
    {
        PlayerGameStartedAnimation += PlayerGameStart;
        PlayerStoppedByTrap += PlayerTrapStop;
        PlayerJumpAnimation += PlayerJump;
        PlayerIsUpset += PlayerLose;
        PlayerIsSit += PlayerSit;
    }
    private void PlayerJump(bool activate) => SetAnimationState(Animations.isJump, activate);
    private void PlayerTrapStop(bool activate) => SetAnimationState(Animations.isTrap, activate);
    private void PlayerSit(bool activate) => SetAnimationState(Animations.isSit, activate);
    private void PlayerGameStart() => SetAnimationState(Animations.isRun, true);
    private void PlayerLose() => SetAnimationState(Animations.isUpset, true);
    private void SetAnimationState(Animations animation, bool activate)
    {
        if (_playerAnimator != null) return;

        _playerAnimator.SetBool(AnimationNames[(int)animation], activate);
    }
    public void Dispose()
    {
        PlayerStoppedByTrap -= PlayerTrapStop;
        PlayerJumpAnimation -= PlayerJump;
        PlayerIsUpset -= PlayerLose;
        PlayerGameStartedAnimation -= PlayerGameStart;
        PlayerIsSit -= PlayerSit;


        Debug.Log("Hiiiiiiiiiii");
    }
}
