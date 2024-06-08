using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
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
    private string[] _animationNames => new string[]
     {
        "isTrap",
        "isRun",
        "isJump",
        "isUpset",
        "isSit"
     };

    private Animator _playerAnimator;

    private void OnEnable()
    {
        PlayerGameStartedAnimation += PlayerGameStart;
        PlayerStoppedByTrap += PlayerTrapStop;
        PlayerJumpAnimation += PlayerJump;
        PlayerIsUpset += PlayerLose;
        PlayerIsSit += PlayerSit;
    }
    private void Start() => _playerAnimator = transform.GetChild(1).GetComponent<Animator>();
    private void PlayerJump(bool activate) => SetAnimationState(Animations.isJump, activate);
    private void PlayerTrapStop(bool activate) => SetAnimationState(Animations.isTrap, activate);
    private void PlayerSit(bool activate) => SetAnimationState(Animations.isSit, activate);
    private void PlayerGameStart() => SetAnimationState(Animations.isRun, true);
    private void PlayerLose() => SetAnimationState(Animations.isUpset, true);
    private void SetAnimationState(Animations animation, bool activate)
    {
        if (_playerAnimator != null) return;

        _playerAnimator.SetBool(_animationNames[(int)animation], activate);
    }
    private void OnDisable()
    {
        PlayerStoppedByTrap -= PlayerTrapStop;
        PlayerJumpAnimation -= PlayerJump;
        PlayerIsUpset -= PlayerLose;
        PlayerGameStartedAnimation -= PlayerGameStart;
        PlayerIsSit -= PlayerSit;
    }
}
