using System.Collections.Generic;
using UnityEngine;

public abstract class Health
{
    protected List<GameObject> _hurts;

    protected HealthFactory _healthFactory;
    protected LivesSettings _livesSettings;

    protected readonly PropUIAnimation _propUIAnim = new();
    protected Health(HealthFactory healthFactory, LivesSettings livesSettings, List<GameObject> hurts)
    {
        _healthFactory = healthFactory;
        _livesSettings = livesSettings;
        _hurts = hurts;
    }

    public abstract void Execute();
}