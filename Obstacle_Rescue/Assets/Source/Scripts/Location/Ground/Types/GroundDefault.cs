using UnityEngine;
using Zenject;

public class GroundDefault : Ground
{
    private BoxCollider2D _collider;

    [Inject] private readonly Player _player;

    private void Awake()
    {
        Initialize();
        CalculateHeight();
        GenerateProps();
    }

    private void FixedUpdate()
    {
        if (_player == null) return;

        Fall();
        Move();
    }

    protected override void Initialize()
    {
        _initialPos = transform;
        Begin = transform.GetChild(0);
        End = transform.GetChild(1);
        _collider = GetComponent<BoxCollider2D>();
    }
    private void CalculateHeight()
    {
        Height = transform.position.y + (_collider.bounds.extents.y);
        Debug.Log($"Height of {gameObject.name}: {Height}");
    }

    protected override void Move()
    {
        transform.Translate(_player.Velocity.x * Time.fixedDeltaTime * Vector2.left);

        if (transform.position.x < Settings.HidePositionX)
            gameObject.SetActive(false);
    }
    protected override void GenerateProps()
    {
        if (transform.GetChild(3).TryGetComponent(out _spawnPropTransform) == true)
            Settings.PropsGenerator.Execute(_spawnPropTransform);
    }

    private void Fall()
    {
        if (!_fallPlatformAdded && Random.Range(0, 10) == Settings.FallPlatformChance)
        {
            if (transform.position.x - 5 == _player.transform.position.x)
            {
                gameObject.AddComponent<GroundFall>().Initialize(_player);
                _fallPlatformAdded = true;
            }
        }
    }
    private void OnDisable() => transform.position = new(0, _initialPos.position.y, 0);
}
