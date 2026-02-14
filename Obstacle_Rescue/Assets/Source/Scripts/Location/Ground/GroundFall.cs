using UnityEngine;

public sealed class GroundFall : MonoBehaviour
{
    private bool shouldFall = false;
    public float FallSpeed { get; private set; } = 5f;
    public float FallAmount { get; private set; }
    private Player _player;
    public void Initialize(Player player) => _player = player;
    public void FixedUpdate()
    {
        if (shouldFall)
        {
            Vector2 pos = transform.position;
            FallAmount = FallSpeed * Time.fixedDeltaTime;
            pos.y -= FallAmount;

            if (_player != null)
            {
                Vector2 playerPos = _player.transform.position;
                playerPos.y -= FallAmount;
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