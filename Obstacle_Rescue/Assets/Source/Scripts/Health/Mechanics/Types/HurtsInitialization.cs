using UnityEngine;

public class HurtsInitialization : Health
{
    private HealthPrefabsLoader _healthPrefabLoader = new();

    public HurtsInitialization(HealthFactory healthFactory, LivesSettings livesSettings)
    {
        _healthFactory = healthFactory;
        _livesSettings = livesSettings;
    }
    public override void Execute()
    {
        LoadHurt();

        Transform transform = _healthFactory.transform;
        startHurts = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            startHurts[i] = transform.GetChild(i).gameObject;

        hurts.AddRange(startHurts);
    }
    private void LoadHurt() => hurt = _healthPrefabLoader.Execute();
}