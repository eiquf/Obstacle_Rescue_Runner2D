using UnityEngine;

public sealed class PlayerObstacle : PlayerSystem
{
    private readonly MovementSettings _movementSettings;
    public PlayerObstacle
        (PlayerAnimation animation,
        MovementSettings movementSettings)
        : base(animation) => _movementSettings = movementSettings;
    public override void Execute(Transform transform)
    {
        Vector2 obstOrigin = new(_pos.x, _pos.y);
        RaycastHit2D[] hits = {
        Physics2D.Raycast(obstOrigin, Vector2.right, _velocity.x * Time.fixedDeltaTime, _movementSettings.ObstacleLayerMask),
        Physics2D.Raycast(obstOrigin, Vector2.up, _velocity.y * Time.fixedDeltaTime, _movementSettings.ObstacleLayerMask)
    };

        foreach (var hit in hits)
        {
            if (hit.collider != null)
                Hit(hit.collider.gameObject);
        }
    }
    private void Hit(GameObject trap)
    {
        Object.Destroy(trap);
        _velocity.x *= _movementSettings.StopVelocity;
    }
}