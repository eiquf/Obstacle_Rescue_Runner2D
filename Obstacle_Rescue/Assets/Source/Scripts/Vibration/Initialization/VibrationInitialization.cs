public sealed class VibrationInitialization : VibrationSettings, IVibration
{
    private readonly int _maxIndex = 1;
    private int _vibrationIndex;

    public void Execute(VibrationController controller)
    {
        VibrationData data = LoadVibrationSettings();
        if (data.FirstPlay != -1)
        {
            _vibrationIndex = _maxIndex;
            data.VibrationImageIndex = 0;
            data.FirstPlay = -1;
        }
        else
            _vibrationIndex = data.VibrationVolume;

        data.VibrationVolume = _vibrationIndex;
        SaveVibrationSettings(data);
    }
}