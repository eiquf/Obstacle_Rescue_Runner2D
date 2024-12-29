using UnityEngine;

public sealed class PlayerInjure : PlayerSystem
{
    private readonly Health _health;
    public PlayerInjure(Player player, Health health) : base(player) => _health = health;
    public override void Execute(Transform transform)
    {
        Vector2 healOrigin = new(_pos.x, _pos.y);
        RaycastHit2D healHitX = Physics2D.Raycast(healOrigin, Vector2.right, _velocity.x * Time.fixedDeltaTime, _player.MovementSettings.HealLayerMask);
        if (healHitX.collider != null)
            _health.OnHealed?.Invoke();

        RaycastHit2D healHitY = Physics2D.Raycast(healOrigin, Vector2.up, _velocity.y * Time.fixedDeltaTime, _player.MovementSettings.HealLayerMask);
        if (healHitY.collider != null)
            _health.OnHealed?.Invoke();
    }
}