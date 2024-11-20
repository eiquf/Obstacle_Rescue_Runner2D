using UnityEngine;

[CreateAssetMenu(fileName = "Ground settings", menuName = "Ground settings")]
public class GroundSettings : ScriptableObject
{
    [field: SerializeField] public int FallPlatformChance { get; private set; } = 4;
    [field: SerializeField] public float HidePositionX { get; private set; } = -200f;
    [field: SerializeField] public int ColliderScaleSizeX { get; private set; } = 4;
    public GroundPropsGenerator PropsGenerator { get; private set; } = new();
}
