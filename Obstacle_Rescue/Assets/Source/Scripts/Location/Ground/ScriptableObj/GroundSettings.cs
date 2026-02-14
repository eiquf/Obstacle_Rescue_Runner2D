using UnityEngine;

[CreateAssetMenu(fileName = "Ground settings", menuName = "Ground settings")]
public class GroundSettings : ScriptableObject
{
    #region DEFAULT
    [SerializeField] private int _fallPlatformChance = 4;
    [SerializeField] private float _hidePositionX = -200f;
    public int FallPlatformChance => _fallPlatformChance;
    public float HidePositionX => _hidePositionX;
    public GroundPropsGenerator PropsGenerator { get; private set; } = new();
    #endregion
    #region CONDITION
    [SerializeField] private bool _enableAdvancedSettings;
    public bool EnableAdvancedSettings => _enableAdvancedSettings;
    #endregion
    #region ADVANCED
    [SerializeField] private int _colliderScaleSizeX = 4;
    public int ColliderScaleSizeX => _colliderScaleSizeX;
    #endregion
}