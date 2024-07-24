using UnityEngine;

public class PlayerTakeHealth : PlayerSystem
{
    private readonly MovementSettings _movementSettings;
    public PlayerTakeHealth
        (PlayerAnimation animation,
        MovementSettings movementSettings) : base(animation) => _movementSettings = movementSettings;

    public override void Execute(Transform transform)
    {
        Vector2 healOrigin = new(_pos.x, _pos.y);
        RaycastHit2D healHitX = Physics2D.Raycast(healOrigin, Vector2.right, _velocity.x * Time.fixedDeltaTime, _movementSettings.HealLayerMask);
        if (healHitX.collider != null)
        {
            if (healHitX.collider.TryGetComponent<Heal>(out var heal))
                heal.IsHealCollected?.Invoke();
        }

        RaycastHit2D healHitY = Physics2D.Raycast(healOrigin, Vector2.up, _velocity.y * Time.fixedDeltaTime, _movementSettings.HealLayerMask);
        if (healHitY.collider != null)
        {
            if (healHitY.collider.TryGetComponent<Heal>(out var heal))
                heal.IsHealCollected?.Invoke();
        }
    }
}