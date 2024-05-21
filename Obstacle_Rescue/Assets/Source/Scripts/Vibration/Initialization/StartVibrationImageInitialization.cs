public class StartVibrationImageInitialization : VibrationSettings, IVibration
{
    public void Execute(VibrationController vibrationController)
    {
        VibrationData data = LoadVibrationSettings();

        vibrationController.image.sprite = vibrationController.ButtonsSprites[data.VibrationImageIndex];

        SaveVibrationSettings(data);
    }
}