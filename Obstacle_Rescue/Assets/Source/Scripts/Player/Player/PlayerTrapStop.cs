using UnityEngine;

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
        Vector2 healOrigin = new(_pos.x, _pos.y);
        bool isStack = Physics2D.Raycast(healOrigin, Vector2.right, _velocity.x * Time.fixedDeltaTime, _movementSettings.TrapsLayerMask).collider != null ||
                       Physics2D.Raycast(healOrigin, Vector2.up, _velocity.y * Time.fixedDeltaTime, _movementSettings.TrapsLayerMask).collider != null;

        if (isStack)
        {
            _velocity.x = _player.Velocity.x * _movementSettings.StopVelocity;
            _player.SetVelocity(_velocity);
            _animation.PlayerIsStop?.Invoke(true);
        }
    }
}