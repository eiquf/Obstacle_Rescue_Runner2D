using UnityEngine;

[CreateAssetMenu(fileName = "Lives settings", menuName = "Lives settings")]
public class LivesSettings : ScriptableObject
{
    [field: Header("Lives")]
    [field: SerializeField] public int MaxLives { get; private set; } = 3;
    [field: SerializeField] public int MinLives { get; private set; } = 0;
}