public class SoundInitialization : SoundType
{
    private const int MaxSound = 1;

    public override void Execute(SoundController soundController)
    {
        SoundData soundData = LoadSoundSettings();

        if (soundData.FirstPlay != -1)
        {
            soundData.BGMVolume = soundData.SFXVolume = MaxSound;
            soundData.BGMButtonImageIndex = 2;
            soundData.SFXButtonImageIndex = 0;
            soundData.FirstPlay = -1;
        }

        ApplyVolumesToAudioSources(soundData, soundController);
        SaveSoundSettings(soundData);
    }

    private void ApplyVolumesToAudioSources(SoundData soundData, SoundController soundController)
    {
        soundController.AudioSources[UIButtonsCount.SFX].volume = soundData.SFXVolume;
        soundController.AudioSources[UIButtonsCount.BGM].volume = soundData.BGMVolume;
    }
}