using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Health : MonoBehaviour, IPlayerObserver
{
    [SerializeField] private LivesSettings _livesSettings;
    [SerializeField] private List<GameObject> _hurts;

    private readonly Dictionary<PlayerStates, Action> actions = new();
    #region Scripts
    private AddHealth _addHealthSystem;
    public RemoveHealth _removeHealthSystem;
    public Death _dead;
    #endregion

    [Inject] private Player _player;
    private void OnEnable()
    {
        new HurtsInitialization(_hurts, transform).Execute();
        _player.AddObserver(this);

        RegisterAction(PlayerStates.Dead, Dead);
        RegisterAction(PlayerStates.Heal, AddHealth);
        RegisterAction(PlayerStates.Injure, RemoveHealth);
    }
    private void OnDisable()
    {
        UnregisterAction(PlayerStates.Dead, Dead);
        UnregisterAction(PlayerStates.Heal, AddHealth);
        UnregisterAction(PlayerStates.Injure, RemoveHealth);
    }
    private void Start() => Initialize();
    private void Initialize()
    {
        _dead = new Death(_hurts);
        _addHealthSystem = new AddHealth(_livesSettings, _hurts, transform);
        _removeHealthSystem = new RemoveHealth(_livesSettings, _hurts, _dead);
    }
    private void AddHealth() => _addHealthSystem.Execute();
    private void RemoveHealth() => _removeHealthSystem.Execute();
    private void Dead() => _dead.Execute();

    #region Actions
    private void RegisterAction(PlayerStates state, Action action)
    {
        if (!actions.ContainsKey(state)) actions[state] = action;
        else actions[state] += action;
    }
    private void UnregisterAction(PlayerStates state, Action action)
    {
        if (actions.ContainsKey(state))
        {
            actions[state] -= action;

            if (actions[state] == null)
                actions.Remove(state);
        }
    }
    public void OnNotify(PlayerStates state)
    {
        if (actions.TryGetValue(state, out var action))
            action.Invoke();
    }
    #endregion
}