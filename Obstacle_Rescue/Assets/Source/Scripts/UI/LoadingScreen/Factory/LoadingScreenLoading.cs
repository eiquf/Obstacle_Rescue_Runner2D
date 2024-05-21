using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class LoadingScreenLoading : LoadingScreen<IEnumerator>
{
    private string _sceneName;
    private readonly LoadingScreenFactory _loadingScreen;
    public LoadingScreenLoading(string name) => _sceneName = name;
    public override IEnumerator Execute()
    {
        yield return new WaitForSeconds(_additiveTimeToWait);

        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(_sceneName, LoadSceneMode.Single);

        yield return handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _loadingScreen.gameObject.SetActive(false);
            Addressables.UnloadSceneAsync(handle);
        }
    }
}