using System;
using UnityEngine;
using Zenject;

public class GroundTrap : Ground
{
    public Action OnCollised { get; private set; }
    public Action OnEnded { get; private set; }

    public string Key => Settings.Word;
    [SerializeField] private WordPanel _panel;
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
        Move();
    }
    private void OnEnable()
    {
        OnCollised += Trap;
        OnEnded += Ended;
    }
    private void OnDisable()
    {
        transform.position = new(0, _initialPos.position.y, 0);
        OnCollised -= Trap;
        OnEnded -= Ended;
    }
    protected override void Initialize()
    {
        _initialPos = transform;
        Begin = transform.GetChild(0);
        End = transform.GetChild(1);
        _collider = GetComponent<BoxCollider2D>();
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
    private void Trap() => _panel.OnAdded?.Invoke(Settings.Word, Settings.Timer, Settings.DottedImage, this);
    private void Ended()
    {
        _player.OnStop?.Invoke(false);
        Debug.Log("Fp");
    }
}
