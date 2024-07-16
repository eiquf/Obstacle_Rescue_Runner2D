using UnityEngine;

public sealed class PlayerDead : PlayerSystem
{
    private readonly HealthFactory _health;
    public PlayerDead(PlayerAnimation animation, HealthFactory health)
        : base(animation) { _health = health; }

    public override void Execute(Transform transform)
    {
        if (_pos.y < -20)
        {
            if (_pos.y < -20f)
                Object.Destroy(transform.gameObject);

            _health.OnPlayerDeath?.Invoke();
        }
    }
}