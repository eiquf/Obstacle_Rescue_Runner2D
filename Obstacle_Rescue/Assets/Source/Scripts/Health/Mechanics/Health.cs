using System.Collections.Generic;
using UnityEngine;

public abstract class Health
{
    protected List<GameObject> _hurts;

    protected LivesSettings _livesSettings;
    protected PlayerDeath _playerDeath;

    protected readonly PropUIAnimation _propUIAnim = new();
    protected Health(LivesSettings livesSettings, List<GameObject> hurts, PlayerDeath playerDeath)
    {
        _livesSettings = livesSettings;
        _hurts = hurts;
        _playerDeath = playerDeath;
    }

    public abstract void Execute();
}