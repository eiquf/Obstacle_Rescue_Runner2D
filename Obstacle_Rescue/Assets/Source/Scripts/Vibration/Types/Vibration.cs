public sealed class Vibration : VibrationSettings, IVibration, IButtonAction
{
    private VibrationController _controller;
    private readonly int _maxVibration = 1, _minVibration = 0;
    public void Execute(VibrationController controller) => _controller = controller;
    public void Execute()
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
        _controller.Image.sprite = _controller.Sprites[data.VibrationImageIndex];
        _controller.SetIndex(data.VibrationVolume);

        SaveVibrationSettings(data);
    }
}