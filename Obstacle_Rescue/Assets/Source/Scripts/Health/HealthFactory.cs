using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HealthFactory : MonoBehaviour
{
    #region Actions
    public Action OnPlayerDeath;
    public Action OnPlayerDamaged;
    public Action OnPlayerHealed;
    #endregion
    [SerializeField] private LivesSettings _livesSettings;
    protected List<GameObject> _hurts = new();
    #region Scripts
    private AddHealth _addHealth;
    private RemoveHealth _removeHealth;
    private PlayerDeath _playerDeath;
    private AliveChecker _aliveChecker;
    #endregion

    #region Injects
    private PlayerAnimation _playerAnimation;
    private MainCameraFactory _cameraController;

    [Inject]
    private void Construct
        (MainCameraFactory cameraController,
        PlayerAnimation playerAnimation)
    {
        _playerAnimation = playerAnimation;
        _cameraController = cameraController;
    }
    #endregion
    private void OnEnable()
    {
        OnPlayerDeath += IsDead;
        OnPlayerDamaged += ChangeHealth;
        OnPlayerHealed += AddHealth;
    }
    private void OnDisable()
    {
        OnPlayerDeath -= IsDead;
        OnPlayerDamaged -= ChangeHealth;
        OnPlayerHealed -= AddHealth;
    }
    private void Start() => HurtsInitialize();
    private void FixedUpdate() => _aliveChecker.Execute();
    private void HurtsInitialize()
    {
        new HurtsInitialization(_livesSettings, null, _hurts, transform).Execute();

        _playerDeath = new PlayerDeath(_livesSettings, _hurts, null, _cameraController, _playerAnimation);
        _addHealth = new AddHealth(_livesSettings, _hurts, null, transform);
        _removeHealth = new RemoveHealth(_livesSettings, _hurts, _playerDeath);
        _aliveChecker = new AliveChecker(_livesSettings, _hurts, _playerDeath);
    }
    private void AddHealth() => _addHealth.Execute();
    private void ChangeHealth() => _removeHealth.Execute();
    private void IsDead() => _playerDeath.Execute();
}