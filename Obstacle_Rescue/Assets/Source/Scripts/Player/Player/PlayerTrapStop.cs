using System;
using UnityEngine;


[Serializable]
public sealed class PlayerTrapStop : PlayerSystem
{
    private MovementSettings _movementSettings;
    private Player _player;
    public PlayerTrapStop
        (MovementSettings movementSettings,
        Player player,
        PlayerAnimation animation)
        : base(animation)
    {
        _movementSettings = movementSettings;
        _player = player;
    }

    public override void Execute(Transform transform) => Hit();
    private void Hit()
    {
        _animation.PlayerStoppedByTrap?.Invoke(true);
        //_player.Velocity.x *= _movementSettings.stopVelocity;
    }
}