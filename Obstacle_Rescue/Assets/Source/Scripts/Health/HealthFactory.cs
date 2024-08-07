using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HealthFactory : MonoBehaviour
{
    #region Actions
    public Action OnPlayerDamaged;
    public Action OnPlayerHealed;
    public Action OnPlayerDead;
    #endregion
    [SerializeField] private LivesSettings _livesSettings;
    [SerializeField] private List<GameObject> _hurts = new();
    #region Scripts
    private AddHealth _addHealth;
    private RemoveHealth _removeHealth;
    private PlayerDead _dead;

    private PlayerAnimation _anim;
    private MainCameraFactory _mainCameraFactory;
    #endregion
    [Inject]
    private void Construct(Player animation, MainCameraFactory camera)
    {
        _anim = animation.GetComponent<PlayerAnimation>();
        _mainCameraFactory = camera;
    }
    private void OnEnable()
    {
        OnPlayerDamaged += RemoveHealth;
        OnPlayerHealed += AddHealth;
        OnPlayerDead += Dead;
    }
    private void OnDisable()
    {
        OnPlayerDamaged -= RemoveHealth;
        OnPlayerHealed -= AddHealth;
        OnPlayerDead -= Dead;
    }
    private void Start() => HurtsInitialize();
    private void HurtsInitialize()
    {
        new HurtsInitialization(_livesSettings, _hurts, transform).Execute();

        _dead = new PlayerDead(_livesSettings, _hurts, _anim, _mainCameraFactory);
        _addHealth = new AddHealth(_livesSettings, _hurts, transform);
        _removeHealth = new RemoveHealth(_livesSettings, _hurts, _dead);
    }
    private void AddHealth() => _addHealth.Execute();
    private void RemoveHealth() => _removeHealth.Execute();
    private void Dead() => _dead.Execute();
}