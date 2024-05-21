using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetsLoader<T>
{
    private readonly string[] _assetNames;

    public AssetsLoader(string[] assetNames) => _assetNames = assetNames;

    public T[] Execute()
    {
        T[] assets = new T[_assetNames.Length];

        for (int i = 0; i < _assetNames.Length; i++)
        {
            int index = i;

            Addressables.LoadAssetAsync<T>(_assetNames[i]).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    assets[index] = handle.Result;
                    Addressables.Release(handle);
                }
            };
        }
        return assets;
    }
}