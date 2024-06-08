using UnityEngine;

public sealed class GroundFall : MonoBehaviour
{
    private bool shouldFall = false;
    public float fallSpeed { get; private set; } = 5f;
    public float fallAmount { get; private set; }
    private Player _player;
    public void SetPlayer(Player player) => _player = player;
    private void FixedUpdate()
    {
        if (shouldFall)
        {
            Vector2 pos = transform.position;
            fallAmount = fallSpeed * Time.fixedDeltaTime;
            pos.y -= fallAmount;

            if (_player != null)
            {
                Vector2 playerPos = _player.transform.position;
                playerPos.y -= fallAmount;
                _player.transform.position = playerPos;
            }

            transform.position = pos;
        }
        else
        {
            if (_player != null)
                shouldFall = true;
        }
    }
}