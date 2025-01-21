public class SFX : SoundType, IButtonAction
{
    private SoundController _controller;
    private SoundData _data;
    
    public override void Execute(SoundController soundController)
    {
        onButtonImageIndex = 0;
        offButtonImageIndex = 1;

        _controller = soundController;
        _data = LoadSoundSettings();
    }

    public void Execute()
    {
        if (_data.SFXVolume == maxVolume)
            _data.SFXVolume = minVolume;
        else
            _data.SFXVolume = maxVolume;

        ApplyChanges(_data, _controller);
    }

    private void ApplyChanges(SoundData _data, SoundController soundController)
    {
        soundController.AudioSources[UIButtonsCount.SFX].volume = _data.SFXVolume;
        _data.SFXButtonImageIndex = _data.SFXVolume == maxVolume ? onButtonImageIndex : offButtonImageIndex;
        soundController.ButtonsImages[UIButtonsCount.SFX].sprite = soundController.Sprites[_data.SFXButtonImageIndex];
        SaveSoundSettings(_data);
    }
}