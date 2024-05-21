using System;
using UnityEngine;

public class HealthFactory : MonoBehaviour
{
    #region Actions
    public Action OnPlayerDeath;
    public Action OnPlayerDamaged;
    public Action OnPlayerHealed;
    #endregion
    [SerializeField] private LivesSettings _livesSettings;
    #region Scripts
    private HurtsInitialization _hurtsInitialize;

    private readonly AddHealth _addHealth = new();
    private readonly RemoveHealth _removeHealth = new();
    private readonly PlayerDeath _playerDeath = new();
    private readonly AliveChecker _aliveChecker = new();
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
        _hurtsInitialize = new HurtsInitialization(this, _livesSettings);
        _hurtsInitialize.Execute();
    }
    private void AddHealth() => _addHealth.Execute();
    private void ChangeHealth() => _removeHealth.Execute();
    private void IsDead() => _playerDeath.Execute();
}