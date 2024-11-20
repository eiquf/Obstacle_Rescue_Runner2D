using System.Collections.Generic;
using UnityEngine;

public abstract class Health
{
    protected List<GameObject> _hurts;

    protected LivesSettings _livesSettings;
    protected Health(LivesSettings livesSettings, List<GameObject> hurts)
    {
        _livesSettings = livesSettings;
        _hurts = hurts;
    }
    public abstract void Execute();
}