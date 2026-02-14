using UnityEngine;

public class SoundSettings
{
    protected SoundData LoadSoundSettings()
    {
        string json = PlayerPrefs.GetString(SoundData.soundSettingsKey, "{}");
        return JsonUtility.FromJson<SoundData>(json);
    }
    protected void SaveSoundSettings(SoundData soundData)
    {
        string json = JsonUtility.ToJson(soundData);
        PlayerPrefs.SetString(SoundData.soundSettingsKey, json);
    }
}