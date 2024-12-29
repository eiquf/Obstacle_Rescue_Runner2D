using UnityEngine;

public abstract class PlayerSystem
{
    protected static Vector2 _pos;
    protected static Vector2 _velocity;
    protected static bool _isGrounded = true;

    protected Player _player;
    public PlayerSystem(Player player) => _player = player;
    public abstract void Execute(Transform transform);
}