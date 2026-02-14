public sealed class VibrationInitialization : VibrationSettings, IVibration
{
    private readonly int _maxIndex = 1;
    public void Execute(VibrationController controller)
    {
        VibrationData data = LoadVibrationSettings();
        if (data.FirstPlay != -1)
        {
            data.VibrationVolume = _maxIndex;
            data.VibrationImageIndex = 0;
            data.FirstPlay = -1;
        }
        controller.Image.sprite = controller.Sprites[data.VibrationImageIndex];
        controller.SetIndex(data.VibrationVolume);
        SaveVibrationSettings(data);
    }
}