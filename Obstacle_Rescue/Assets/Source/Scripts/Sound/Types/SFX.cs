public class SFX : SoundType
{
    public SFX()
    {
        minVolume = 0;
        maxVolume = 1;
        onButtonImageIndex = 2;
        offButtonImageIndex = 3;
    }

    public override void Execute(SoundController soundController)
    {
        SoundData soundData = LoadSoundSettings();

        if (soundData.SFXVolume == maxVolume)
            soundData.SFXVolume = minVolume;
        else
            soundData.SFXVolume = maxVolume;

        ApplyChanges(soundData, soundController);
    }

    private void ApplyChanges(SoundData soundData, SoundController soundController)
    {
        soundController.AudioSources[UIButtonsCount.SFX].volume = soundData.SFXVolume;
        soundController.ButtonsImages[UIButtonsCount.SFX].sprite = soundController.Sprites[soundData.SFXVolume == maxVolume ? onButtonImageIndex : offButtonImageIndex];
        SaveSoundSettings(soundData);
    }
}