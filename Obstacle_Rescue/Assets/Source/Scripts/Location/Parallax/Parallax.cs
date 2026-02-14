using UnityEngine;
using Zenject;

public sealed class Parallax : MonoBehaviour
{
    [SerializeField] private ParallaxSettings _parallaxSettings;

    private Transform[] _transforms;
    private Vector3[] _initialPositions;
    private float _velocity;

    [Inject] private Player _player;

    private void Start()
    {
        _transforms = new Transform[transform.childCount];
        _initialPositions = new Vector3[transform.childCount];

        for (int i = 0; i < _transforms.Length; i++)
        {
            _transforms[i] = transform.GetChild(i);
            _initialPositions[i] = _transforms[i].position;
        }
    }

    private void FixedUpdate()
    {
        ParallaxMove();

        for (int i = 0; i < _transforms.Length; i++)
        {
            Vector3 newPosition = _initialPositions[i];
            newPosition.x += _velocity * Time.fixedDeltaTime;
            _transforms[i].position = newPosition;

            if (_transforms[i].position.x <= _parallaxSettings.hidePosition)
            {
                newPosition.x = _parallaxSettings.spawnPosition;
                _transforms[i].position = newPosition;
            }
        }
    }

    private void ParallaxMove() => _velocity = _player.Velocity.x / _parallaxSettings.Depth;
}
