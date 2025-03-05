using UnityEngine;

public sealed class PlayerGet : PlayerSystem
{
    private readonly AnimationContext _context = new();
    private readonly ItemPopAnimation _anim = new();
    public PlayerGet(Player player) : base(player) { }
    public override void Execute(Transform transform)
    {
        _pos = transform.position;
        CheckCollisions();
        _context.SetAnimationStrategy(_anim);
    }
    private void CheckCollisions()
    {
        Vector2 rayOrigin = new(_pos.x, _pos.y - 0.5f);
        float rayDistance = 3.5f;

        Vector2[] directions = { Vector2.right, Vector2.up, Vector2.down, Vector2.left };

        foreach (Vector2 direction in directions)
        {
            RaycastHit2D healHit = Physics2D.Raycast(rayOrigin, direction, rayDistance, _player.MovementSettings.HealLayerMask);
            RaycastHit2D letterHit = Physics2D.Raycast(rayOrigin, direction, rayDistance, _player.MovementSettings.LetterLayerMask);
            if (healHit.collider != null)
            {
                _player.OnNotify?.Invoke(PlayerStates.Heal);
                _context.PlayAnimation(healHit.collider.transform);
                break;
            }
            else if (letterHit.collider != null)
            {
                _player.OnNotify?.Invoke(PlayerStates.Letter);
                _context.PlayAnimation(letterHit.collider.transform);
                break;
            }
        }
    }
}