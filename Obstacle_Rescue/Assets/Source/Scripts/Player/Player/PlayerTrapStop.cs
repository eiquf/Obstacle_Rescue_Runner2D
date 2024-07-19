using System;
using UnityEngine;


[Serializable]
public sealed class PlayerTrapStop : PlayerSystem
{
    private readonly MovementSettings _movementSettings;
    private readonly Player _player;

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
        _animation.PlayerIsStop?.Invoke();
        _velocity.x = _player.Velocity.x * _movementSettings.StopVelocity;
        _player.SetVelocity(_velocity);
    }
}