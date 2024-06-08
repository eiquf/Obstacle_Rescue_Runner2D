using UnityEngine;

public class SoundLoader : IComponentsLoader<AudioClip>
{
    private readonly string[] _audioClipsName = new string[]
    {

    };
    public AudioClip[] Execute()
    {
        AssetsLoader<AudioClip> loader = new(_audioClipsName);
        AudioClip[] buttonsSprites = loader.Execute();
        return buttonsSprites;
    }
}