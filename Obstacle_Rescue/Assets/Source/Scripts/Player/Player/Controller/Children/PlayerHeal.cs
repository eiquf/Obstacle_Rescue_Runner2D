using UnityEngine;

public sealed class PlayerHeal : PlayerSystem
{
    public PlayerHeal(Player player) : base(player) { }
    public override void Execute(Transform transform)
    {
        Vector2 healOrigin = new(_pos.x, _pos.y);
        Vector2[] directions = { Vector2.right, Vector2.up };

        foreach (Vector2 direction in directions)
        {
            RaycastHit2D healHit = Physics2D.Raycast(healOrigin, direction, Vector2.Dot(_velocity, direction) * Time.fixedDeltaTime, _player.MovementSettings.HealLayerMask);
            if (healHit.collider != null)
            {
                _player.OnNotify(PlayerStates.Heal);
                break;
            }
        }
    }
}