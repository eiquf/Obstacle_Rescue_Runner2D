using UnityEngine;

public class VibrationSpritesLoader : IComponentsLoader<Sprite>
{
    private string[] _spritesNames = new string[]
    {
        "VibrationOn",
        "VibrationOff"
    };

    public Sprite[] Execute()
    {
        AssetsLoader<Sprite> loader = new(_spritesNames);
        Sprite[] buttonsSprites = loader.Execute();
        return buttonsSprites;
    }
}