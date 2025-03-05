using UnityEditor;
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
    [SerializeField] private int _timer;
    [SerializeField] private string _word;
    [SerializeField] private Sprite _dottedImage;
    public int ColliderScaleSizeX => _colliderScaleSizeX;
    public int Timer => _timer;
    public string Word => _word;
    public Sprite DottedImage => _dottedImage;
    #endregion
}

[CanEditMultipleObjects()]
[CustomEditor(typeof(GroundSettings), true)]
public class GroundSettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GroundSettings settings = (GroundSettings)target;

        Section("DEFAULT PROPERTYS");
        PropertyField("_fallPlatformChance");
        PropertyField("_hidePositionX");
        Section("CONDITION");
        PropertyField("_enableAdvancedSettings");

        if (settings.EnableAdvancedSettings)
        {
            PropertyField("_colliderScaleSizeX");
            PropertyField("_timer");
            PropertyField("_word");
            PropertyField("_ground");
            PropertyField("_dottedImage");
        }
        EditorGUI.BeginChangeCheck();
        serializedObject.ApplyModifiedProperties();
    }

    protected void PropertyField(string path)
    {
        var obj = serializedObject.FindProperty(path);
        EditorGUILayout.PropertyField(obj);
    }
    protected void Space() => EditorGUILayout.Separator();
    protected void Section(string label)
    {
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
    }
}

