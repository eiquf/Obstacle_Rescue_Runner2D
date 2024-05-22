using System;
using System.Collections.Generic;
using UnityEngine;

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
    private HurtsInitialization _hurtsInitialize;

    private AddHealth _addHealth;
    private RemoveHealth _removeHealth;
    private PlayerDeath _playerDeath;
    private AliveChecker _aliveChecker;
    #endregion

    #region Injects
    //private PlayerAnimation _playerAnimation;
    //private MainCamera _cameraController;

    //[Inject]
    //private void Construct
    //    (MainCamera cameraController,
    //    PlayerAnimation playerAnimation)
    //{
    //    _playerAnimation = playerAnimation;
    //    _cameraController = cameraController;
    //}
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
        _hurtsInitialize = new HurtsInitialization(this, _livesSettings, _hurts);
        _hurtsInitialize.Execute();

        _addHealth = new AddHealth(this, _livesSettings, _hurts);
        _playerDeath = new PlayerDeath(this, _livesSettings, _hurts);
        _removeHealth = new RemoveHealth(this, _livesSettings, _hurts, _playerDeath);
        _aliveChecker = new AliveChecker(this, _livesSettings, _hurts, _playerDeath);
    }
    private void AddHealth() => _addHealth.Execute();
    private void ChangeHealth() => _removeHealth.Execute();
    private void IsDead() => _playerDeath.Execute();
}