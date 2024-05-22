using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HurtsInitialization : Health
{
    private GameObject[] startHurts;


    public HurtsInitialization(HealthFactory healthFactory, LivesSettings livesSettings, List<GameObject> hurts) : base(healthFactory, livesSettings, hurts)
    {
    }

    public override void Execute()
    {
        Transform transform = _healthFactory.transform;
        startHurts = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            startHurts[i] = transform.GetChild(i).gameObject;

        _hurts.AddRange(startHurts);
    }
}