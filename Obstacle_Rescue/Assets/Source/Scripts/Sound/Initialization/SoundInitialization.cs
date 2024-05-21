public class SoundInitialization : SoundType
{
    private readonly int _maxSound = 1;
    private int _musicInt, _soundEffectsInt;

    public override void Execute(SoundController soundController)
    {
        SoundData soundData = LoadSoundSettings();

        if (soundData.FirstPlay != -1)
            InitializeFirstPlayValues(soundData);
        else
            InitializeFromSoundData(soundData);

        ApplyVolumesToAudioSources(soundData, soundController);

        SaveSoundSettings(soundData);
    }
    private void InitializeFirstPlayValues(SoundData soundData)
    {
        _musicInt = _maxSound;
        _soundEffectsInt = _maxSound;

        soundData.BGMVolume = _musicInt;
        soundData.SFXVolume = _soundEffectsInt;

        soundData.FirstPlay = -1;
    }
    private void InitializeFromSoundData(SoundData soundData)
    {
        _musicInt = soundData.BGMVolume;
        _soundEffectsInt = soundData.SFXVolume;
    }
    private void ApplyVolumesToAudioSources(SoundData soundData, SoundController soundController)
    {
        soundController.AudioSources[UIButtonsCount.SFX].volume = soundData.SFXVolume;
        soundController.AudioSources[UIButtonsCount.BGM].volume = soundData.BGMVolume;
    }
}