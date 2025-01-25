using System;
using UnityEngine;
using Zenject;

public sealed class Player : MonoBehaviour
{
    #region Actions
    public Action<bool> IsStop { get; private set; }
    #endregion

    [field: SerializeField] public MovementSettings MovementSettings { get; private set; }
    public readonly CharacterAnimation Animation = new();

    private Animator _animator;
    private Transform _shadowTransform;
    private SpriteRenderer _spriteRenderer;

    private readonly PlayerShadow _shadow = new();
    private PlayerMove _move;
    private PlayerObstacle _obstacle;
    private PlayerStop _stop;
    private PlayerInjure _heal;

    private bool _cantMove;
    public Vector2 Velocity { get; private set; }

    private Health _health;
    private GameCamera _mainCamera;

    [Inject]
    private void Construct(Health health, GameCamera mainCamera)
    {
        _health = health;
        _mainCamera = mainCamera;
    }

    private void OnEnable()
    {
        IsStop += Stop;
    }

    private void OnDisable()
    {
        IsStop -= Stop;
        Animation.Dispose();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _shadowTransform = transform.GetChild(0);
        _spriteRenderer = GetComponent<SpriteRenderer>();

        Animation.Inject(_animator, _spriteRenderer);

        Initialize();
    }

    private void FixedUpdate() => Moves();

    private void Initialize()
    {
        _move = new PlayerMove(this, _mainCamera, _health);
        _obstacle = new PlayerObstacle(this);
        _stop = new PlayerStop(this);
        _heal = new PlayerInjure(this, _health);
    }

    private void Moves()
    {
        if (!_cantMove)
        {
            _shadow.Execute(_shadowTransform);
            _move.Execute(transform);
            _heal.Execute(transform);
            _obstacle.Execute(transform);
        }
    }

    public void SetVelocity(Vector2 velocity) => Velocity = velocity;

    private void Stop(bool statement) => _stop.Execute(transform);
}