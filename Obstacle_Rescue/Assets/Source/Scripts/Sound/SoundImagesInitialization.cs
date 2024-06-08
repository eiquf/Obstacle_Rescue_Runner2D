public class SoundImagesInitialization : SoundSettings, ISound
{
    private bool _firstTime = false;

    public SoundImagesInitialization() => _firstTime = false;
    public void Execute(SoundController soundController)
    {
        SoundData soundData = LoadSoundSettings();

        int indexBGM = _firstTime ? soundData.BGMButtonImageIndex : 0;
        int indexSFX = _firstTime ? soundData.SFXButtonImageIndex : 2;

        soundController.ButtonsImages[UIButtonsCount.SFX].sprite = soundController.Sprites[indexSFX];
        soundController.ButtonsImages[UIButtonsCount.BGM].sprite = soundController.Sprites[indexBGM];

        _firstTime = true;
        SaveSoundSettings(soundData);
    }
}