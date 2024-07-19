using UnityEngine;

public abstract class PlayerSystem
{
    protected  PlayerAnimation _animation;

    protected static Vector2 _pos;
    protected static Vector2 _velocity;
    protected static bool _isGrounded = true;
    public PlayerSystem(PlayerAnimation animation) => _animation = animation;
    public abstract void Execute(Transform transform);
}