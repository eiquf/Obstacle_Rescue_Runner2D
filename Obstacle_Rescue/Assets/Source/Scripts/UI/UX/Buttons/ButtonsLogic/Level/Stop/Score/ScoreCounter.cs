using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class ScoreCounter : MonoBehaviour, IPlayerObserver
{
    public Action<Text> OnShow;
    private int _score = 0;
    private readonly Dictionary<PlayerStates, IScoreStrategy> _strategies = new();
    [Inject] private readonly Player _player;

    private void OnEnable()
    {
        RegisterStrategy(PlayerStates.Heal, new AddScoreStrategy(10));
        RegisterStrategy(PlayerStates.Injure, new RemoveScoreStrategy(5));
        OnShow += ShowScore;
    }
    private void OnDisable()
    {
        UnregisterStrategy(PlayerStates.Heal);
        UnregisterStrategy(PlayerStates.Injure);
        OnShow -= ShowScore;
    }
    private void Start() => _player.AddObserver(this);
    private void ShowScore(Text text) => text.text = _score.ToString();

    #region Strategy Management
    private void RegisterStrategy(PlayerStates state, IScoreStrategy strategy)
    {
        if (!_strategies.ContainsKey(state))
            _strategies[state] = strategy;
    }
    private void UnregisterStrategy(PlayerStates state)
    {
        if (_strategies.ContainsKey(state))
            _strategies.Remove(state);
    }
    public void OnNotify(PlayerStates state)
    {
        if (_strategies.TryGetValue(state, out var strategy))
            _score = strategy.CalculateScore(_score);
    }
    #endregion
}
