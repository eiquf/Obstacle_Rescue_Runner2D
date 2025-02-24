using UnityEngine;

public class LetterMovement : IWord<Vector2, Transform>
{
    private readonly Player _player;
    public LetterMovement(Player player) => _player = player;
    public Vector2 Execute(Transform pos) => pos.position *= _player.Velocity.x * Time.fixedDeltaTime;
}
