using UnityEngine;

public sealed class PlayerObstacle : PlayerSystem
{
    public PlayerObstacle(Player player) : base(player) { }
    public override void Execute(Transform transform) => Hit(transform);
    private void Hit(Transform trap)
    {
        Object.Destroy(trap.gameObject);
        _velocity.x *= _player.MovementSettings.StopVelocity;
    }
}