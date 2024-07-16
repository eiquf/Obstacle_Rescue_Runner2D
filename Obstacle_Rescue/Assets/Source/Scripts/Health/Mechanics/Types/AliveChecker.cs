using System.Collections.Generic;
using UnityEngine;

public class AliveChecker : Health
{
    public AliveChecker
        (LivesSettings livesSettings,
        List<GameObject> hurts,
        PlayerDeath playerDeath)
        : base(livesSettings, hurts, playerDeath) { }
    public override void Execute()
    {
        if (_hurts.Count == _livesSettings.MinLives)
            _playerDeath.Execute();
    }
}