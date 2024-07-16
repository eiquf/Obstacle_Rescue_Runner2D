using System;
using UnityEngine;
using Zenject;

public sealed class Player : MonoBehaviour
{
    #region Actions
    public Action IsHitted { get; private set; }
    public Action IsRun { get; private set; }
    public Action<bool> ShouldStop { get; private set; }
    public Action IsMove { get; private set; }
    #endregion
    [SerializeField] private bool _canMove = false;
    public Vector2 Velocity { get; private set; }
    private Transform _shadowTransform;
    private Animator _animator;
    #region Components
    private PlayerMove Move { get; set; }
    private PlayerObstacle Obstacle { get; set; }
    private PlayerTrapStop Trap { get; set; }
    private PlayerDead Dead { get; set; }
    private PlayerTakeHealth Heal { get; set; }
    private PlayerShadow Shadow { get; set; }
    #endregion

    private Player _player => this;
    private PlayerAnimation _animation = new();
    private MovementSettings _movementSettings;
    private HealthFactory _health;
    private MainCameraFactory _mainCamera;

    [Inject]
    private void Construct
        (MovementSettings movementSettings,
        HealthFactory health,
        MainCameraFactory mainCamera)
    {
        _movementSettings = movementSettings;
        _health = health;
        _mainCamera = mainCamera;
    }
    private void OnEnable()
    {
        IsHitted += Hit;
        IsRun += CanMove;
        IsMove += Moves;
    }
    private void OnDisable()
    {
        IsHitted -= Hit;
        IsRun -= CanMove;
        IsMove -= Moves;
    }
    private void Awake()
    {
        InitializeControlComponents();
        _shadowTransform = transform.GetChild(0);
        _animator = transform.GetChild(1).GetComponent<Animator>();
    }
    public void SetVelocity(Vector2 velocity) => Velocity = velocity;
    private void InitializeControlComponents()
    {
        Move = new PlayerMove
            (_movementSettings,
            _player,
            _animation,
            _mainCamera);

        Obstacle = new PlayerObstacle
            (_animation,
            _movementSettings);

        Trap = new PlayerTrapStop
            (_movementSettings,
            _player,
            _animation);

        Dead = new PlayerDead
            (_animation,
            _health);

        Heal = new PlayerTakeHealth
            (_animation,
            _movementSettings);

        Shadow = new PlayerShadow(_animation);
    }
    private void CanMove() => _canMove = true;
    private void Hit() => Obstacle.Execute(transform);
    private void FixedUpdate() => Moves();
    private void Moves()
    {
        if (_canMove == true)
        {
            Shadow.Execute(_shadowTransform);
            Trap.Execute(transform);
            Move.Execute(transform);
            Heal.Execute(transform);
            Dead.Execute(transform);
        }
    }
}