using System.Collections.Generic;
using UnityEngine;

public class AliveChecker : Health
{
    private PlayerDeath _playerDeath;

    public AliveChecker
        (HealthFactory healthFactory,
        LivesSettings livesSettings,
        List<GameObject> hurts,
        PlayerDeath playerDeath)
        : base(healthFactory, livesSettings, hurts)
    { _playerDeath = playerDeath; }

    public override void Execute()
    {
        if (_hurts.Count == _livesSettings.MinLives)
            _playerDeath.Execute();
    }
}