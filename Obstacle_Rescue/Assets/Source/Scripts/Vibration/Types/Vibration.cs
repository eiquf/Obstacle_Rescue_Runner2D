public sealed class Vibration : VibrationSettings, IVibration
{
    private readonly int _maxVibration = 1;
    private readonly int _minVibration = 0;

    public void Execute(VibrationController vibrationController)
    {
        VibrationData data = LoadVibrationSettings();

        if (data.VibrationVolume == _maxVibration)
        {
            data.VibrationVolume = _minVibration;
            data.VibrationImageIndex = 1;
        }
        else
        {
            data.VibrationVolume = _maxVibration;
            data.VibrationImageIndex = 0;
        }
        vibrationController.image.sprite = vibrationController.ButtonsSprites[data.VibrationImageIndex];

        SaveVibrationSettings(data);
    }
}