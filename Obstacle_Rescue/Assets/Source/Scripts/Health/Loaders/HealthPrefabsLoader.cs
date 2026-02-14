using UnityEngine;

public sealed class HealthSystemPrefabsLoader: IComponentLoader<GameObject>
{
    private readonly string _prefabName = "Hurt";

    public GameObject Execute()
    {
        AssetLoader<GameObject> loader = new(_prefabName);
        GameObject buttonsSprites = loader.Execute();
        return buttonsSprites;
    }
}