using UnityEngine;

public class GroundPlatformsSetter : IComponentsLoader<GameObject>
{
    private readonly string[] _prefabsAPI = new string[]
    {
        "DefPlatformSec 1",
        "Default Platform V1",
        "Default Platform V2",
        "DefaultPlatformV3",
        "DefPlatformSec 1",
        "DefPlatformSec 2",
        "DefPlatformSec",
        "Boat Platform",
        "Bridge Platform",
        "BucketPlatform"
    };
    private void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (array[j], array[i]) = (array[i], array[j]);
        }
    }
    public GameObject[] Execute()
    {
        Shuffle(_prefabsAPI);

        AssetsLoader<GameObject> loader = new(_prefabsAPI);
        GameObject[] loadedGrounds = loader.Execute();
        return loadedGrounds;
    }
}