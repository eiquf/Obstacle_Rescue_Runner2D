using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class Player : MonoBehaviour
{
    public Action<bool> OnStop {  get; private set; } 
    public Action<PlayerStates> OnNotify { get; private set; }
    private readonly List<IPlayerObserver> observers = new();
    [field: SerializeField] public MovementSettings MovementSettings { get; private set; }
    [SerializeField] private bool _cantMove = false;
    public CharacterAnimation Animation { get; private set; }

    private Animator _animator;
    private Transform _shadowTransform;
    private SpriteRenderer _spriteRenderer;

    #region Components
    private readonly PlayerShadow _shadow = new();
    private PlayerMove _move;
    private PlayerObstacle _obstacle;
    private PlayerStop _stop;
    private PlayerGet _stuff;
    private PlayerDead _dead;
    #endregion

    #region Positions data
    public Vector2 Velocity { get; private set; }
    public float MinY { get; private set; }
    public float MaxY { get; private set; }
    #endregion

    private void OnEnable()
    {
        EventBus.OnGameStopped += Stop;
        OnStop += Stop;
        OnNotify += NotifyObservers;
    }
    private void OnDisable()
    {
        OnStop -= Stop;
        EventBus.OnGameStopped -= Stop;

        OnNotify -= NotifyObservers;
        Animation.Dispose();
    }

    private void Start()
    {
        _cantMove = true;

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

        _move = new(this);
        _obstacle = new(this);
        _stop = new(this);
        _stuff = new(this);
        _dead = new(this);
    }
    private void Moves()
    {
        if (_cantMove == true) return;

        _shadow.Execute(_shadowTransform);
        _move.Execute(transform);
        _stuff.Execute(transform);
        _obstacle.Execute(transform);
        _dead.Execute(transform);
    }
    public void SetVelocity(Vector2 velocity) => Velocity = velocity;
    public void SetYPos(float maxY, float minY)
    {
        MaxY = maxY; 
        MinY = minY;
    }
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
    public void AddObserver(IPlayerObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
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
    Dead, Stop, Move, Heal, Injure, Fall, Complete, Letter, Trap
}