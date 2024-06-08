using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GroundPlatformsSetter : IComponentsInitialize<GameObject>
{
    private readonly string[] _prefabsAPI = new string[]
    {
        "Default Platform V1",
        "Default Platform V2",
        "Default Platform V3",
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

        int targetCount = _prefabsAPI.Length;
        int loadedCount = 0;

        GameObject[] loadedGrounds = new GameObject[targetCount];
        for (int i = 0; i < targetCount; i++)
        {
            int index = i;
            Addressables.LoadAssetAsync<GameObject>(_prefabsAPI[i]).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject loadedGround = handle.Result;
                    Addressables.Release(handle);
                    loadedCount++;
                }
            };
        }

        return loadedGrounds;
    }
}