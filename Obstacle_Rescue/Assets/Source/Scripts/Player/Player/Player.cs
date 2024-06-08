using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerAnimation))]
public sealed class Player : MonoBehaviour
{
    #region Actions
    public Action IsHitted { get; private set; }
    public Action IsRun { get; private set; }
    public Action<bool> ShouldStop { get; private set; }
    private event Action IsMove;
    #endregion
    [SerializeField] private bool _canMove = false;
    public Vector2 Velocity { get; private set; }
    private Transform _shadowTransform;
    #region Components
    private PlayerMove move { get; set; }
    private PlayerObstacle obstacle { get; set; }
    private PlayerTrapStop trap { get; set; }
    private PlayerDead dead { get; set; }
    private PlayerTakeHealth heal { get; set; }
    private PlayerShadow shadow { get; set; }
    #endregion

    private Player _player => this;
    private PlayerAnimation _animation;
    private MovementSettings _movementSettings;
    private HealthFactory _health;
    private MainCameraFactory _mainCamera;

    [Inject]
    private void Construct
        (PlayerAnimation playerAnimation,
        MovementSettings movementSettings,
        HealthFactory health,
        MainCameraFactory mainCamera)
    {
        _animation = playerAnimation;
        _movementSettings = movementSettings;
        _health = health;
        _mainCamera = mainCamera;
    }
    private void OnEnable()
    {
        IsHitted += Hit;
        IsRun += CanMove;
        IsMove += Move;
    }
    private void OnDisable()
    {
        IsHitted -= Hit;
        IsRun -= CanMove;
        IsMove -= Move;
    }
    private void Awake()
    {
        InitializeControlComponents();
        _shadowTransform = transform.GetChild(0);
    }
    public void SetVelocity(Vector2 velocity) => Velocity = velocity;
    private void InitializeControlComponents()
    {
        move = new PlayerMove
            (_movementSettings,
            _player,
            _animation,
            _mainCamera);

        obstacle = new PlayerObstacle
            (_animation,
            _movementSettings);

        trap = new PlayerTrapStop
            (_movementSettings,
            _player,
            _animation);

        dead = new PlayerDead
            (_animation,
            _health);

        heal = new PlayerTakeHealth
            (_animation,
            _movementSettings);

        shadow = new PlayerShadow(_animation);
    }
    private void CanMove() => _canMove = true;
    private void Hit() => obstacle.Execute(transform);
    private void FixedUpdate() => Move();
    private void Move()
    {
        if (_canMove == true)
        {
            shadow.Execute(_shadowTransform);
            trap.Execute(transform);
            move.Execute(transform);
            heal.Execute(transform);
            dead.Execute(transform);
        }
    }
}