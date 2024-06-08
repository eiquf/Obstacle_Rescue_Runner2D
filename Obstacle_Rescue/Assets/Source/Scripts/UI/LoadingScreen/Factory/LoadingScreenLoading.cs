using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class LoadingScreenLoading : LoadingScreen<IEnumerator>
{
    private readonly string _sceneName;
    private readonly LoadingScreenFactory _loadingScreen;
    public LoadingScreenLoading(string name) => _sceneName = name;
    public override async Task Execute()
    {
        await Task.Delay(TimeSpan.FromSeconds(_additiveTimeToWait));

        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(_sceneName, LoadSceneMode.Single);

        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _loadingScreen.gameObject.SetActive(false);
            Addressables.UnloadSceneAsync(handle);
        }
    }
}