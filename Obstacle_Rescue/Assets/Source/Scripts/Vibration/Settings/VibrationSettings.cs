using UnityEngine;

public class VibrationSettings
{
    protected VibrationData LoadVibrationSettings()
    {
        string json = PlayerPrefs.GetString(VibrationData.vibrationKey, "{}");
        return JsonUtility.FromJson<VibrationData>(json);
    }
    protected void SaveVibrationSettings(VibrationData vibrationData)
    {
        string json = JsonUtility.ToJson(vibrationData);
        PlayerPrefs.SetString(VibrationData.vibrationKey, json);
    }
}