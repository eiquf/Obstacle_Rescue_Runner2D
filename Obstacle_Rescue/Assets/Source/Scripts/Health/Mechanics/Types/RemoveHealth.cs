using System.Collections.Generic;
using UnityEngine;

public class RemoveHealth : Health
{
    private readonly PlayerDead _dead;
    public RemoveHealth
        (LivesSettings livesSettings,
        List<GameObject> hurts,
        PlayerDead dead)
        : base(livesSettings, hurts) => _dead = dead;

    public override void Execute()
    {
        if (_hurts.Count <= _livesSettings.MaxLives && _hurts.Count != _livesSettings.MinLives)
        {
            Object.Destroy(_hurts[0], 0.25f);
            _propUIAnim.OnDisable(_hurts[0].transform);
            _hurts.Remove(_hurts[0]);
        }
        else _dead.Execute();
    }
}