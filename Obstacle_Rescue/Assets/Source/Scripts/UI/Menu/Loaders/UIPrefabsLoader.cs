using UnityEngine;

public class UIPrefabsLoader : IComponentsLoader<GameObject>
{
    private string[] _panelsNames = new[]
    {
        "LevelUI",
        "MainMenuUI"
    };
    public GameObject[] Execute()
    {
        AssetsLoader<GameObject> loader = new(_panelsNames);
        GameObject[] prefabs = loader.Execute();
        return prefabs;
    }
}
