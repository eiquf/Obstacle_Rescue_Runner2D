using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerObstacle : PlayerSystem
{
    private readonly AnimationContext _context = new();
    private readonly ItemPopAnimation _anim = new();
    private HashSet<Collider2D> _processedColliders = new();
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

        RaycastHit2D hitX = Physics2D.Raycast(rayOrigin, rayDirectionX, rayDistance, _player.MovementSettings.ObstacleLayerMask);
        RaycastHit2D hitY = Physics2D.Raycast(rayOrigin, rayDirectionY, rayDistance, _player.MovementSettings.ObstacleLayerMask);

        HashSet<Collider2D> uniqueHits = new();
        if (hitX.collider != null) uniqueHits.Add(hitX.collider);
        if (hitY.collider != null) uniqueHits.Add(hitY.collider);

        foreach (Collider2D collider in uniqueHits)
        {
            if (_processedColliders.Contains(collider)) continue;

            _processedColliders.Add(collider);
            _player.OnNotify?.Invoke(PlayerStates.Injure);
            _player.Animation.IsInjured();
            _context.PlayAnimation(collider.transform);
            ChangeVelocity();
        }
    }
    private void ChangeVelocity()
    {
        _velocity.x = _player.Velocity.x * _player.MovementSettings.StopVelocity;
        _player.SetVelocity(_velocity);
    }
}