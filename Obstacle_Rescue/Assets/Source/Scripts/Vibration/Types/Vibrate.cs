using UnityEngine;

public class Vibrate : VibrationSettings, IButtonAction
{
    private readonly int _maxVibration = 1;
    public void Execute()
    {
        VibrationData data = LoadVibrationSettings();

        if (data.VibrationVolume == _maxVibration)
            Handheld.Vibrate();
        SaveVibrationSettings(data);
    }
}
