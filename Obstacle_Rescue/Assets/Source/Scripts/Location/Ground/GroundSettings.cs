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
    public int ColliderScaleSizeX => _colliderScaleSizeX;
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
        PropertyField("_colliderScaleSizeX");

        //if(settings.)
        //SerializedProperty advancedSettingsProp = serializedObject.FindProperty("enableAdvancedSettings");
        //EditorGUILayout.PropertyField(advancedSettingsProp);

        //if (advancedSettingsProp.boolValue)
        //{
        //    EditorGUILayout.PropertyField(serializedObject.FindProperty("advancedSetting1"));
        //    EditorGUILayout.PropertyField(serializedObject.FindProperty("advancedSetting2"));
        //}
        EditorGUI.BeginChangeCheck();
        if (EditorGUI.EndChangeCheck()) serializedObject.ApplyModifiedProperties();
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

