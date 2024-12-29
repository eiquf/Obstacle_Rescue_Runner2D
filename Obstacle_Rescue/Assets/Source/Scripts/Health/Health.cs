using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Health : MonoBehaviour
{
    #region Actions
    public Action OnDamaged;
    public Action OnHealed;
    public Action OnDeath;
    #endregion
    [SerializeField] private LivesSettings _livesSettings;
    [SerializeField] private readonly List<GameObject> _hurts = new();
    #region Scripts
    private AddHealth _addHealthSystem;
    private RemoveHealth _removeHealthSystem;
    private Death _dead;

    private GameCamera _camera;
    private Player _player;
    #endregion
    [Inject]
    private void Container(GameCamera camera, Player player)
    {
        _camera = camera;
        _player = player;
    }
    private void OnEnable()
    {
        OnDamaged += RemoveHealth;
        OnHealed += AddHealth;
        OnDeath += Dead;
    }
    private void OnDisable()
    {
        OnDamaged -= RemoveHealth;
        OnHealed -= AddHealth;
        OnDeath -= Dead;
    }
    private void Start() => Initialize();
    private void Initialize()
    {
        new HurtsInitialization(_hurts, transform).Execute();

        _dead = new Death(_hurts, _camera, _player);
        _addHealthSystem = new AddHealth(_livesSettings, _hurts, transform);
        _removeHealthSystem = new RemoveHealth(_livesSettings, _hurts, _dead);
    }
    private void AddHealth() => _addHealthSystem.Execute();
    private void RemoveHealth() => _removeHealthSystem.Execute();
    private void Dead() => _dead.Execute();
}