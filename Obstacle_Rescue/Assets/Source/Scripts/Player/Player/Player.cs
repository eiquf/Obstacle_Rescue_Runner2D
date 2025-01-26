using System;
using UnityEngine;
using Zenject;

public sealed class Player : MonoBehaviour
{
    #region Actions
    public Action OnSlowDown;
    public Action<bool> IsStop { get; private set; }
    #endregion

    [field: SerializeField] public MovementSettings MovementSettings { get; private set; }
    private bool _cantMove = false;
    public CharacterAnimation Animation { get; private set; }

    private Animator _animator;
    private Transform _shadowTransform;
    private SpriteRenderer _spriteRenderer;

    private readonly PlayerShadow _shadow = new();
    private PlayerMove _move;
    private PlayerObstacle _obstacle;
    private PlayerStop _stop;
    private PlayerInjure _heal;

    public Vector2 Velocity { get; private set; }

    private Health _health;
    private GameCamera _mainCamera;

    [Inject]
    private void Construct(Health health, GameCamera mainCamera)
    {
        _health = health;
        _mainCamera = mainCamera;
    }

    private void OnEnable() => IsStop += Stop;

    private void OnDisable()
    {
        IsStop -= Stop;
        Animation.Dispose();
    }

    private void Start()
    {
        Velocity = Vector2.zero;

        _animator = GetComponent<Animator>();
        _shadowTransform = transform.GetChild(0);
        _spriteRenderer = GetComponent<SpriteRenderer>();

        Initialize();
    }

    private void FixedUpdate() => Moves();

    private void Initialize()
    {
        Animation = new(_animator, _spriteRenderer);
        Animation.Initialize();

        _move = new PlayerMove(this, _mainCamera, _health);
        _obstacle = new PlayerObstacle(this);
        _stop = new PlayerStop(this);
        _heal = new PlayerInjure(this, _health);
    }
    private void Moves()
    {
        if (_cantMove == true) return;

        _shadow.Execute(_shadowTransform);
        _move.Execute(transform);
        _heal.Execute(transform);
        _obstacle.Execute(transform);
    }
    public void SetVelocity(Vector2 velocity) => Velocity = velocity;
    private void Stop(bool statement)
    {
        _cantMove = statement;
        if (statement)
            _stop.Execute(transform);
    }
}