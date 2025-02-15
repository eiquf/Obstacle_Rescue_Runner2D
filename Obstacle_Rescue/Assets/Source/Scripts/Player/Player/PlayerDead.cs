using UnityEngine;

public class PlayerDead : PlayerSystem
{
    public PlayerDead(Player player) : base(player) { }
    public override void Execute(Transform transform)
    {
        if (transform.position.y < -20f)
        {
            _player.OnNotify?.Invoke(PlayerStates.Dead);
            Object.Destroy(_player.gameObject);
        }
    }
}