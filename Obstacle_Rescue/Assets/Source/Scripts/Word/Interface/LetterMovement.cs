using UnityEngine;

public class LetterMovement : IWord<Transform, Transform>
{
    private readonly Player _player;
    public LetterMovement(Player player) => _player = player;
    public Transform Execute(Transform pos)
    {
        pos.position *= _player.Velocity.x* Time.fixedDeltaTime;
        return pos;
    }
}
