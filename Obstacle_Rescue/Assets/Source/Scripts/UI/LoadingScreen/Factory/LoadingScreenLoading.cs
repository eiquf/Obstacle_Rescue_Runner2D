using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class LoadingScreenLoading : LoadingScreen<string>
{
    private readonly LoadingScreenFactory _loadingScreen;
    public LoadingScreenLoading(LoadingScreenFactory loadingScreenFactory) => _loadingScreen = loadingScreenFactory;

    public override async Task Execute(string thing)
    {
        await Task.Delay(TimeSpan.FromSeconds(_additiveTimeToWait));

        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(thing, LoadSceneMode.Single);

        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _loadingScreen.gameObject.SetActive(false);
            SceneManager.UnloadSceneAsync(handle.Result.Scene);
        }
    }
}