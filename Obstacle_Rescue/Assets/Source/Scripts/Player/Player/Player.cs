using System;
using UnityEngine;
using Zenject;

public sealed class Player : MonoBehaviour
{
    #region Actions
    public Action<bool> IsStop { get; private set; }
    public Action<Transform> IsSlowDown { get; private set; }
    #endregion
    [field: SerializeField] public MovementSettings MovementSettings { get; private set; }
    private readonly CharacterAnimation _animation = new();
    public CharacterAnimation Animation
    {
        get { return _animation; }
        private set { Animation = _animation; }
    }

    [SerializeField] private bool _cantMove = false;
    public Vector2 Velocity { get; private set; }
    private Transform _shadowTransform;
    private Animator _animator;

    #region Components
    private readonly PlayerShadow _shadow = new();

    private PlayerMove _move;
    private PlayerObstacle _slowDown;
    private PlayerStop _stop;
    private PlayerInjure _heal;

    #endregion  
    private Player _player => this;
    private Health _health;
    private GameCamera _mainCamera;

    [Inject]
    private void Construct
        (Health health,
        GameCamera mainCamera)
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
        _move = new(_player, _mainCamera);
        _slowDown = new(_player);
        _stop = new(_player);
        _heal = new(_player, _health);
        Animation.Inject(_animator);
    }
    private void Moves()
    {
        if (_cantMove != true)
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