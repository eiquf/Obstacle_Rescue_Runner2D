using UnityEngine;

public class PlayerTakeHealth : PlayerSystem
{
    private readonly MovementSettings _movementSettings;
    private readonly HealthFactory _health;

    public PlayerTakeHealth
        (PlayerAnimation animation,
        MovementSettings movementSettings,
        HealthFactory health) : base(animation)
    {
        _movementSettings = movementSettings;
        _health = health;
    }

    public override void Execute(Transform transform)
    {
        Vector2 healOrigin = new(_pos.x, _pos.y);
        RaycastHit2D healHitX = Physics2D.Raycast(healOrigin, Vector2.right, _velocity.x * Time.fixedDeltaTime, _movementSettings.HealLayerMask);
        if (healHitX.collider != null)
            _health.OnPlayerHealed?.Invoke();

        RaycastHit2D healHitY = Physics2D.Raycast(healOrigin, Vector2.up, _velocity.y * Time.fixedDeltaTime, _movementSettings.HealLayerMask);
        if (healHitY.collider != null)
            _health.OnPlayerHealed?.Invoke();
    }
}