using UnityEngine;
using Zenject;

public class GroundTrap : Ground
{
    [SerializeField] private WordPanel _panel;
    [SerializeField] private GameObject _objToActivate;
    public BoxCollider2D _collider;

    [SerializeField] LayerMask _layerMask;
    [Inject] private readonly Player _player;

    private void Awake()
    {
        Initialize();
        CalculateHeight();
        GenerateProps();

        _collider.enabled = false;
    }
    private void FixedUpdate()
    {
        if (_player == null) return;
        Move();
    }
    private void OnDisable() => transform.position = new(0, _initialPos.position.y, 0);
    protected override void Initialize()
    {
        //_collider = GetComponent<BoxCollider2D>();
        _initialPos = transform;
        Begin = transform.GetChild(0);
        End = transform.GetChild(1);
    }
    private void CalculateHeight() => Height = transform.position.y + (_collider.bounds.extents.y);
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
    public void Trap()
    {
        _panel.gameObject.SetActive(true);
        _panel.Activate(this);
    }
    public void SetObj() => _objToActivate.SetActive(true);
}
