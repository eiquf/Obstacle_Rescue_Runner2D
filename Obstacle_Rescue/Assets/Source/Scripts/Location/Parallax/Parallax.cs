using UnityEngine;
using Zenject;

public sealed class Parallax : MonoBehaviour
{
    private Vector2 _position;
    private float _velocity;

    [SerializeField] private ParallaxSettings _parallaxSettings;
    private Player _playerMove;

    [Inject]
    private void Construct(Player playerMove) => _playerMove = playerMove;
    private void FixedUpdate()
    {
        ParallaxMove();

        _position.x -= _velocity * Time.fixedDeltaTime;

        if (_position.x <= _parallaxSettings.hidePosition)
            _position.x = _parallaxSettings.spawnPosition;

        transform.position = _position;
    }

    private void ParallaxMove()
    {
        _velocity = _playerMove.Velocity.x / _parallaxSettings.Depth;
        _position = transform.position;
    }
}