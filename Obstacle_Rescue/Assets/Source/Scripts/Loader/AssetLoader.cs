using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetLoader<T>
{
    private readonly string _assetName;
    private T asset;

    public AssetLoader(string assetName) => _assetName = assetName;

    public T Execute()
    {
        Addressables.LoadAssetAsync<T>(_assetName).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                asset = handle.Result;
                Addressables.Release(handle);
            }
        };
        return asset;
    }
}