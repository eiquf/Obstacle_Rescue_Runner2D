using UnityEngine;

public sealed class PlayerObstacle : PlayerSystem
{
    private readonly AnimationContext _context = new();
    private readonly ItemPopAnimation _anim = new();
    public PlayerObstacle(Player player) : base(player) { }
    public override void Execute(Transform transform)
    {
        _pos = transform.position;
        CheckCollision();
        _context.SetAnimationStrategy(_anim);
    }
    private void CheckCollision()
    {
        Vector2 rayOrigin = new(_pos.x, _pos.y - 0.5f);
        float rayDistance = 1f;

        Vector2 rayDirectionX = Vector2.right;
        Vector2 rayDirectionY = Vector2.down;

        RaycastHit2D[] hits =
        {
            Physics2D.Raycast(rayOrigin, rayDirectionX, rayDistance, _player.MovementSettings.ObstacleLayerMask),
             Physics2D.Raycast(rayOrigin, rayDirectionY, rayDistance, _player.MovementSettings.ObstacleLayerMask)
        };

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                _player.Animation.IsInjured();
                _context.PlayAnimation(hit.collider.transform);
                ChangeVelocity();
            }
        }

        Debug.DrawRay(rayOrigin, rayDirectionX * rayDistance, Color.green);
    }
    private void ChangeVelocity()
    {
        _velocity.x = _player.Velocity.x * _player.MovementSettings.StopVelocity;
        _player.SetVelocity(_velocity);
    }
}