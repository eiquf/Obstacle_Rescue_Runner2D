using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class Player : MonoBehaviour
{
    #region Actions
    public Action OnSlowDown;
    public Action<PlayerStates> OnNotify;
    public Action<bool> IsStop { get; private set; }
    #endregion
    private readonly List<IPlayerObserver> observers = new();
    [field: SerializeField] public MovementSettings MovementSettings { get; private set; }
    private bool _cantMove = false;
    public CharacterAnimation Animation { get; private set; }

    private Animator _animator;
    private Transform _shadowTransform;
    private SpriteRenderer _spriteRenderer;

    #region Components
    private readonly PlayerShadow _shadow = new();
    private PlayerMove _move;
    private PlayerObstacle _obstacle;
    private PlayerStop _stop;
    private PlayerHeal _heal;
    #endregion
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
        OnNotify += NotifyObservers;
    }

    private void OnDisable()
    {
        IsStop -= Stop;

        OnNotify -= NotifyObservers;
        Animation.Dispose();
    }

    private void Start()
    {
        Velocity = Vector2.zero;

        _animator = GetComponent<Animator>();
        _shadowTransform = transform.GetChild(0);
        _spriteRenderer = GetComponent<SpriteRenderer>();

        Initialize();

        AddObserver(_health);
    }

    private void FixedUpdate() => Moves();

    private void Initialize()
    {
        Animation = new(_animator, _spriteRenderer);
        Animation.Initialize();

        _move = new(this, _mainCamera);
        _obstacle = new(this);
        _stop = new(this);
        _heal = new(this);
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
        {
            _stop.Execute(transform);
            NotifyObservers(PlayerStates.Stop);
        }
    }
    #region Observers
    private void AddObserver(IPlayerObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }
    private void RemoveObserver(IPlayerObserver observer)
    {
        if (observers.Contains(observer))
            observers.Remove(observer);
    }
    private void NotifyObservers(PlayerStates state)
    {
        foreach (var observer in observers)
            observer.OnNotify(state);
    }
    #endregion
}
public interface IPlayerObserver
{
    void OnNotify(PlayerStates state);
}
public enum PlayerStates
{
    Dead, Stop, Move, Heal, Injure
}