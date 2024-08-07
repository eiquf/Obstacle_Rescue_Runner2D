using System.Collections.Generic;
using UnityEngine;

public class HurtsInitialization : Health
{
    private readonly Transform _spawn;
    private GameObject[] startHurts;
    public HurtsInitialization
        (LivesSettings livesSettings,
        List<GameObject> hurts,
        Transform transform)
        : base(livesSettings, hurts) => _spawn = transform;

    public override void Execute()
    {
        Transform transform = _spawn.transform;
        startHurts = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            startHurts[i] = transform.GetChild(i).gameObject;

        foreach (GameObject hurt in startHurts)
            _propUIAnim.OnEnable(hurt.transform);

        _hurts.AddRange(startHurts);
    }
}