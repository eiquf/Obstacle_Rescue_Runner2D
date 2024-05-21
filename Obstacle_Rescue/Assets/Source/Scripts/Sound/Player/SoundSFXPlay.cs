public class SoundSFXPlay
{
    private SoundController _controller;
    private int _index;
    public SoundSFXPlay(SoundController soundController, int index)
    {
        _controller = soundController;
        _index = index;

        Execute();
    }
    private void Execute() => _controller.AudioSources[1].PlayOneShot(_controller.AudioClips[_index]);
}