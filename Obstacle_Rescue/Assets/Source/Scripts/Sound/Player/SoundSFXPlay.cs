using System;

public class SoundSFXPlay : IDisposable
{
    public Action SFXPlay { get; set; }

    private readonly SoundController _controller;
    public SoundSFXPlay(SoundController soundController)
    {
        _controller = soundController;

        SFXPlay += SoundPlay;
    }
    public void Dispose() => SFXPlay -= SoundPlay;
    private void SoundPlay() => _controller.AudioSources[1].PlayOneShot(_controller.AudioClip);
}
