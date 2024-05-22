using System.Collections.Generic;
using UnityEngine;

public class RemoveHealth : Health
{
    private PlayerDeath _playerDeath;

    public RemoveHealth
        (HealthFactory healthFactory,
        LivesSettings livesSettings,
        List<GameObject> hurts,
        PlayerDeath playerDeath)
        : base(healthFactory, livesSettings, hurts)
    { _playerDeath = playerDeath; }

    public override void Execute()
    {
        if (_hurts.Count <= _livesSettings.MaxLives && _hurts.Count != _livesSettings.MinLives)
        {
            Object.Destroy(_hurts[0], 0.25f);
            _propUIAnim.OnDisable(_hurts[0].transform);
            _hurts.Remove(_hurts[0]);
        }
        else
            _playerDeath.Execute();
    }
}