using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HurtsInitialization : Health
{
    private readonly Transform _spawn;
    private GameObject[] startHurts;
    public HurtsInitialization
        (LivesSettings livesSettings, 
        PlayerDeath playerDeath, 
        List<GameObject> hurts, 
        Transform transform) 
        : base(livesSettings, hurts, playerDeath)
    { _spawn = transform; }

    public override void Execute()
    {
        Transform transform = _spawn.transform;
        startHurts = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            startHurts[i] = transform.GetChild(i).gameObject;

        _hurts.AddRange(startHurts);
    }
}