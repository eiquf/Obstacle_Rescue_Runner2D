using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class LoadScene : LoadingTask<string>
{
    private const int _additiveTimeToWait = 1;
    private string _sceneName;
    public override async Task Execute(string name)
    {
        _sceneName = name == LevelsKeys.mainMenuLevelKey ? LevelsKeys.levelKey : LevelsKeys.mainMenuLevelKey;
        await Task.Delay(TimeSpan.FromSeconds(_additiveTimeToWait));

        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(_sceneName, LoadSceneMode.Single);

        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
            SceneManager.UnloadSceneAsync(handle.Result.Scene);
    }
}