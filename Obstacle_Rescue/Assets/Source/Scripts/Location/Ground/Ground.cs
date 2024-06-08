using UnityEngine;

public sealed class Ground : MonoBehaviour
{
    private const int IndexOfFallPlatform = 4;
    private const int HidePos = -200;

    private bool _isFallWasAdded = false;
    public Transform Begin { get; private set; }
    public Transform End { get; private set; }
    private BoxCollider2D _collider;
    public float Height { get; private set; }

    [SerializeField] private bool _canChangePointsHeight;

    private Player _playerMove;
    public void Inject(Player playerController) => _playerMove = playerController;
    private void Awake()
    {
        Begin = transform.GetChild(0);
        End = transform.GetChild(1);

        _collider = GetComponent<BoxCollider2D>();
        Height = transform.position.y + (_collider.size.y / 2);
    }
    private void FixedUpdate()
    {
        if (_playerMove == null) return;

        if (!_isFallWasAdded)
        {
            int randomIndex = Random.Range(0, 10);
            if (randomIndex == IndexOfFallPlatform)
                gameObject.AddComponent<GroundFall>();

            _isFallWasAdded = true;
        }

        MoveGround();
    }
    private void MoveGround()
    {
        Vector2 pos = transform.position;
        pos.x -= _playerMove.Velocity.x * Time.fixedDeltaTime;

        if (transform.position.x < HidePos)
            Destroy(gameObject);

        transform.position = pos;
    }
}