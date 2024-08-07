using System.Collections.Generic;
using UnityEngine;

public class AddHealth : Health
{
    private readonly HealthPrefabsLoader _healthPrefabLoader = new();
    private readonly Transform _spawn;
    private GameObject hurt;
    public AddHealth
        (LivesSettings livesSettings,
        List<GameObject> hurts,
        Transform transform)
        : base(livesSettings, hurts)
    {
        _spawn = transform;
        LoadHurt();
    }
    public override void Execute()
    {
        if (_hurts.Count < _livesSettings.MaxLives && _hurts.Count != _livesSettings.MinLives)
        {
            GameObject hurtPref = Object.Instantiate(hurt, _spawn.transform);
            _propUIAnim.OnEnable(hurtPref.transform);
            _hurts.Add(hurtPref);
        }
    }
    private void LoadHurt() => hurt = _healthPrefabLoader.Execute();
}