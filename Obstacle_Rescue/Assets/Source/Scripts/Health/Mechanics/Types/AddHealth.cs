using System.Collections.Generic;
using UnityEngine;

public class AddHealth : Health
{
    private readonly HealthPrefabsLoader _healthPrefabLoader = new();
    private GameObject hurt;
    public AddHealth(HealthFactory healthFactory, LivesSettings livesSettings, List<GameObject> hurts) : base(healthFactory, livesSettings, hurts) { LoadHurt(); }
    public override void Execute()
    {
        if (_hurts.Count < _livesSettings.MaxLives && _hurts.Count != _livesSettings.MinLives)
        {
            GameObject hurtPref = Object.Instantiate(hurt, _healthFactory.transform);
            _propUIAnim.OnEnable(hurtPref.transform);
            _hurts.Add(hurtPref);
        }
    }
    private void LoadHurt() => hurt = _healthPrefabLoader.Execute();
}