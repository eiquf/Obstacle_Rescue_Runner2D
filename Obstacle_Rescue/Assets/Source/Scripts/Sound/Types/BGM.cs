public class BGM : SoundType, IButtonAction
{
    private SoundController _controller;
    private SoundData _data;
    public override void Execute(SoundController soundController)
    {
        onButtonImageIndex = 2;
        offButtonImageIndex = 3;

        _controller = soundController;
        _data = LoadSoundSettings();   
    }

    public void Execute()
    {
        if (_data.BGMVolume == maxVolume)
            _data.BGMVolume = minVolume;
        else
            _data.BGMVolume = maxVolume;

        ApplyChanges(_data, _controller);
    }

    private void ApplyChanges(SoundData _data, SoundController soundController)
    {
        soundController.AudioSources[UIButtonsCount.BGM].volume = _data.BGMVolume;
        _data.BGMButtonImageIndex = _data.BGMVolume == maxVolume ? onButtonImageIndex : offButtonImageIndex;
        soundController.ButtonsImages[UIButtonsCount.BGM].sprite = soundController.Sprites[_data.BGMButtonImageIndex];
        SaveSoundSettings(_data);
    }
}