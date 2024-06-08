using UnityEngine;

public sealed class PlayerObstacle : PlayerSystem
{
    private MovementSettings _movementSettings;
    public PlayerObstacle
        (PlayerAnimation animation,
        MovementSettings movementSettings)
        : base(animation) => _movementSettings = movementSettings;
    public override void Execute(Transform transform)
    {
        Vector2 obstOrigin = new Vector2(_pos.x, _pos.y);
        RaycastHit2D obstHitX = Physics2D.Raycast(obstOrigin, Vector2.right, _velocity.x * Time.fixedDeltaTime, _movementSettings.obstacleLayerMask);
        if (obstHitX.collider != null)
        {
            Obstacle obstacle = obstHitX.collider.GetComponent<Obstacle>();
            if (obstacle != null)
                Hit(obstacle);
        }

        RaycastHit2D obstHitY = Physics2D.Raycast(obstOrigin, Vector2.up, _velocity.y * Time.fixedDeltaTime, _movementSettings.obstacleLayerMask);
        if (obstHitY.collider != null)
        {
            Obstacle obstacle = obstHitY.collider.GetComponent<Obstacle>();
            if (obstacle != null)
                Hit(obstacle);
        }
    }
    private void Hit(Obstacle obstacle)
    {
        obstacle.IsDestroyed?.Invoke();
        _velocity.x *= _movementSettings.stopVelocity;
    }
}