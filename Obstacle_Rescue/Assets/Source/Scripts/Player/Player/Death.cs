using System.Collections.Generic;
using UnityEngine;

public sealed class Death : HealthSystem
{
    public Death(List<GameObject> hurts, LivesSettings livesSettings = null) : base(livesSettings, hurts) { }
    public override void Execute()
    {
        foreach (var hurt in _hurts)
            Object.Destroy(hurt);

        _hurts.Clear();
    }
}