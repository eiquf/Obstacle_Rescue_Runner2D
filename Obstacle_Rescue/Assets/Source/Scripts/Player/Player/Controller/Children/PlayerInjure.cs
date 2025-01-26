using UnityEngine;

public sealed class PlayerInjure : PlayerSystem
{
    private readonly Health _health;
    public PlayerInjure(Player player, Health health) : base(player) => _health = health;
    public override void Execute(Transform transform)
    {
        Vector2 healOrigin = new(_pos.x, _pos.y);
        Vector2[] directions = { Vector2.right, Vector2.up };

        foreach (Vector2 direction in directions)
        {
            RaycastHit2D healHit = Physics2D.Raycast(healOrigin, direction, Vector2.Dot(_velocity, direction) * Time.fixedDeltaTime, _player.MovementSettings.HealLayerMask);
            if (healHit.collider != null)
            {
                _health.OnHealed?.Invoke();
                break;
            }
        }
    }
}