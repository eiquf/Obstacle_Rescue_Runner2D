public class BGM: SoundType
{
    public BGM()
    {
        minVolume = 0;
        maxVolume = 1;
        onButtonImageIndex = 0;
        offButtonImageIndex = 1;
    }
    public override void Execute(SoundController soundController)
    {
        SoundData soundData = LoadSoundSettings();

        if (soundData.BGMVolume == maxVolume)
            soundData.BGMVolume = minVolume;
        else
            soundData.BGMVolume = maxVolume;

        ApplyChanges(soundData, soundController);
    }
    private void ApplyChanges(SoundData soundData, SoundController soundController)
    {
        soundController.AudioSources[UIButtonsCount.BGM].volume = soundData.BGMVolume;
        soundController.ButtonsImages[UIButtonsCount.BGM].sprite = soundController.Sprites[soundData.BGMVolume == maxVolume ? onButtonImageIndex : offButtonImageIndex];
        SaveSoundSettings(soundData);
    }
}