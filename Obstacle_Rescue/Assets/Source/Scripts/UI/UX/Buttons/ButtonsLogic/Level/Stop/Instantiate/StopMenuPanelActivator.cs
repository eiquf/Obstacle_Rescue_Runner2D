using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class StopMenuPanelActivator : IUIPanelsInstantiate
{
    private const string NAME = "StopPanel";

    private bool _gameIsStopped = false;
    private bool _panelIsCreated = false;

    private Transform _spawnPos;

    private StopMenu _stopMenu;

    public void Execute(Transform spawn)
    {
        if (!_gameIsStopped)
        {
            if (!_panelIsCreated)
            {
                _spawnPos = spawn;
                LoadPanel();
            }
            else
            {
                _stopMenu.gameObject.SetActive(true);
                _stopMenu.IsPanelActivated?.Invoke(true);
            }
        }
        else _stopMenu.IsPanelActivated?.Invoke(false);

        _gameIsStopped = !_gameIsStopped;
    }
    private void LoadPanel() => Addressables.LoadAssetAsync<GameObject>(NAME).Completed += OnLoadDone;

    private void OnLoadDone(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject loadedPrefab = obj.Result;
            GameObject pref = Object.Instantiate(loadedPrefab, _spawnPos);
            _stopMenu = pref.GetComponent<StopMenu>();
            _panelIsCreated = true;
        }
        else
            Debug.LogError("Failed to load addressable: " + NAME);
    }
}