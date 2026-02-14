using System.Collections.Generic;
using UnityEngine;

public abstract class HealthSystem
{
    protected static List<GameObject> _hurts;
    protected LivesSettings _livesSettings;
    protected HealthSystem
        (LivesSettings livesSettings,
        List<GameObject> hurts)
    {
        _livesSettings = livesSettings;
        _hurts = hurts;
    }
    public abstract void Execute();
}