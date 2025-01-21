using UnityEngine.UI;

public sealed class Vibration : VibrationSettings, IButtonAction
{
    private Image _image;
    private readonly SceneChecker _sceneChecker = new();

    private readonly VibrationController _controller;
    private readonly int _maxVibration = 1;
    private readonly int _minVibration = 0;

    public Vibration(VibrationController controller)
    {
        _controller = controller;
        _sceneChecker.OnNotify += Clear;
    }
    public void SetImage(Image image)
    {
        _image = image;
        _image.sprite = _controller.ButtonsSprites[LoadVibrationSettings().VibrationImageIndex];
        _sceneChecker.OnNotify -= Clear;
    }
    public void Execute()
    {
        _sceneChecker.Execute();

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
        _image.sprite = _controller.ButtonsSprites[data.VibrationImageIndex];

        SaveVibrationSettings(data);
    }
    private void Clear() => _image = null;
}