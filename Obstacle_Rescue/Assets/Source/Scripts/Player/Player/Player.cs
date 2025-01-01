using System;
using UnityEngine;
using Zenject;

public sealed class Player : MonoBehaviour
{
    #region Actions
    public Action<bool> IsStop { get; private set; }
    public Action<Transform> IsSlowDown { get; private set; }
    #endregion

    [field:SerializeField] public MovementSettings MovementSettings { get; private set; }
    private readonly CharacterAnimation _animation = new();
    public CharacterAnimation Animation
    {
        get { return _animation; }
        private set { Animation = _animation; }
    }

    private Animator _animator;
    private Transform _shadowTransform;

    private readonly PlayerShadow _shadow = new();
    private PlayerMove _move;
    private PlayerObstacle _slowDown;
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
        IsSlowDown += Slow;
        IsStop += Stop;
    }

    private void OnDisable()
    {
        IsSlowDown -= Slow;
        IsStop -= Stop;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _shadowTransform = transform.GetChild(0);

        Initialize();
    }

    private void FixedUpdate() => Moves();

    private void Initialize()
    {
        _move = new PlayerMove(this, _mainCamera, _health);
        _slowDown = new PlayerObstacle(this);
        _stop = new PlayerStop(this);
        _heal = new PlayerInjure(this, _health);
        _animation.Inject(_animator);
    }

    private void Moves()
    {
        if (!_cantMove)
        {
            _shadow.Execute(_shadowTransform);
            _move.Execute(transform);
            _heal.Execute(transform);
        }
    }

    public void SetVelocity(Vector2 velocity) => Velocity = velocity;

    private void Stop(bool statement) => _stop.Execute(transform);
    private void Slow(Transform trap) => _slowDown.Execute(trap);
}