using UnityEditor;

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
            PropertyField("_colliderScaleSizeX");
        EditorGUI.BeginChangeCheck();
        serializedObject.ApplyModifiedProperties();
    }

    protected void PropertyField(string path)
    {
        var obj = serializedObject.FindProperty(path);
        if (obj != null)
            EditorGUILayout.PropertyField(obj);
    }
    protected void Space() => EditorGUILayout.Separator();
    protected void Section(string label)
    {
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
    }
}