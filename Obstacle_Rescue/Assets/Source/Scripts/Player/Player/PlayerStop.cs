using UnityEngine;

public sealed class PlayerStop : PlayerSystem
{
    public PlayerStop(Player player) : base(player) { }
    public override void Execute(Transform transform) => Hit();
    private void Hit()
    {
        _velocity.x = 0;
        _player.SetVelocity(_velocity);
        _player.Animation.IsStop?.Invoke(true);
    }
}