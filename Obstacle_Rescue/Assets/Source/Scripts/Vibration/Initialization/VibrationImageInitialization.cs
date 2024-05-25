public class VibrationImageInitialization : VibrationSettings, IVibration
{
    public void Execute(VibrationController vibrationController)
    {
        VibrationData data = LoadVibrationSettings();

        vibrationController.Image.sprite = vibrationController.ButtonsSprites[data.VibrationImageIndex];

        SaveVibrationSettings(data);
    }
}