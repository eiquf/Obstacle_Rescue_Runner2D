using System.Collections.Generic;
using UnityEngine;

public abstract class Health
{
    protected GameObject hurt;

    protected GameObject[] startHurts;
    protected List<GameObject> hurts = new();

    protected HealthFactory _healthFactory;
    protected LivesSettings _livesSettings;

    protected readonly PropUIAnimation _propUIAnim = new();

    public abstract void Execute();
}