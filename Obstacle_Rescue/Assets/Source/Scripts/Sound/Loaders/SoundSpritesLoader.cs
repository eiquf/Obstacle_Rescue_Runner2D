using UnityEngine;

public class SoundSpritesLoader : IComponentsLoader<Sprite>
{
    private readonly string[] _spritesName = new string[]
    {
        "SFXOn",
        "SFXOff",
        "BGMOn",
        "BGMOff"
    };

    public Sprite[] Execute()
    {
        AssetsLoader<Sprite> loader = new(_spritesName);
        Sprite[] buttonsSprites = loader.Execute();
        return buttonsSprites;
    }
}