using UnityEngine;

public sealed class Ground : MonoBehaviour
{
    private const int IndexOfFallPlatform = 4;
    private const int HidePos = -200;
    private bool _isFallPlatformAdded = false;

    private Transform _spawnPropTransform;
    private Player _playerController;

    public Transform Begin { get; private set; }
    public Transform End { get; private set; }
    public float Height { get; private set; }

    private BoxCollider2D _collider;

    [SerializeField] private bool _canChangePointsHeight;

    public void Inject(Player playerController) => _playerController = playerController;

    private void Awake()
    {
        CacheComponents();
        CalculateHeight();
        GenerateProps();
    }
    private void CacheComponents()
    {
        Begin = transform.GetChild(0);
        End = transform.GetChild(1);
        _collider = GetComponent<BoxCollider2D>();
    }
    private void CalculateHeight() => Height = transform.position.y + (_collider.size.y / 2);
    private void GenerateProps()
    {
        if (transform.Find("SpawnPoint").TryGetComponent(out _spawnPropTransform))
            new GroundPropsGenerator().Execute(_spawnPropTransform);
    }
    private void FixedUpdate()
    {
        if (_playerController == null) return;

        TryAddFallPlatform();
        MoveGround();
    }
    private void TryAddFallPlatform()
    {
        if (!_isFallPlatformAdded)
        {
            int randomIndex = Random.Range(0, 10);
            if (randomIndex == IndexOfFallPlatform)
            {
                GroundFall groundFall = gameObject.AddComponent<GroundFall>();
                groundFall.Initialize(_playerController);
            }

            _isFallPlatformAdded = true;
        }
    }
    private void MoveGround()
    {
        Vector2 pos = transform.position;
        pos.x -= _playerController.Velocity.x * Time.fixedDeltaTime;
        transform.position = pos;

        if (transform.position.x < HidePos)
            Destroy(gameObject);
    }
}