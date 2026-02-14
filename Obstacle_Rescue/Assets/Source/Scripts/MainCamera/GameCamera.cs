using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class GameCamera : MonoBehaviour, IPlayerObserver
{
    private readonly Dictionary<PlayerStates, Action> actions = new();

    private Camera _camera;
    [Inject] private readonly Player _player;

    private CameraShake _cameraShake;
    private CameraZoom _cameraZoom;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _cameraShake = new(_camera);
        _cameraZoom = new(_camera);
    }

    private void OnEnable()
    {
        _player.AddObserver(this);

        RegisterAction(PlayerStates.Dead, () => _cameraZoom.Execute(true));
        RegisterAction(PlayerStates.Fall, () => _cameraShake.Execute(true));
        RegisterAction(PlayerStates.Complete, () => _cameraZoom.Execute(false));
        RegisterAction(PlayerStates.Move, () => _cameraShake.Execute(false));
    }

    private void OnDisable()
    {
        UnregisterAction(PlayerStates.Dead, () => _cameraZoom.Execute(true));
        UnregisterAction(PlayerStates.Fall, () => _cameraShake.Execute(true));
        UnregisterAction(PlayerStates.Complete, () => _cameraZoom.Execute(false));
        UnregisterAction(PlayerStates.Move, () => _cameraShake.Execute(false));
    }

    private void RegisterAction(PlayerStates state, Action action)
    {
        if (actions.ContainsKey(state))
            actions[state] += action;
        else
            actions[state] = action;
    }

    private void UnregisterAction(PlayerStates state, Action action)
    {
        if (actions.ContainsKey(state))
        {
            actions[state] -= action;
            if (actions[state] == null) actions.Remove(state);
        }
    }
    public void OnNotify(PlayerStates state)
    {
        if (actions.TryGetValue(state, out var action))
            action.Invoke();
    }
}
