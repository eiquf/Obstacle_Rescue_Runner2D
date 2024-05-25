public class SoundImagesInitialization : SoundSettings, ISound
{
    public void Execute(SoundController soundController)
    {
        SoundData soundData = LoadSoundSettings();

        soundController.ButtonsImages[UIButtonsCount.SFX].sprite = soundController.Sprites[soundData.SFXButtonImageIndex];
        soundController.ButtonsImages[UIButtonsCount.BGM].sprite = soundController.Sprites[soundData.BGMButtonImageIndex];

        SaveSoundSettings(soundData);
    }
}