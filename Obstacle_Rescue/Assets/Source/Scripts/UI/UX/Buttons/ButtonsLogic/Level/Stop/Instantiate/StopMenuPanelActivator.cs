using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class StopMenuPanelActivator : IButtonAction
{
    private const string NAME = "StopPanel";

    private readonly Transform _spawnPosition;

    private bool _gameIsStopped = false;
    private bool _panelIsCreated = false;

    private StopMenu _stopMenu;
    private Preferences _preferences;
    public StopMenuPanelActivator(Transform spawnPosition, Transform prefPos)
    {
        _spawnPosition = spawnPosition;
        _preferences = new(prefPos);
    }
    public void Execute()
    {
                _preferences.Execute();
        if (!_gameIsStopped)
        {
            if (!_panelIsCreated)
                LoadPanel();
            else
            {
                _stopMenu.gameObject.SetActive(true);
                _stopMenu.IsPanelActivated?.Invoke(true);
            }

        }
        else _stopMenu.IsPanelActivated?.Invoke(false);

        _gameIsStopped = !_gameIsStopped;
    }
    private void LoadPanel()
    {
        Addressables.LoadAssetAsync<GameObject>(NAME).Completed += OnLoadDone;
    }
    private void OnLoadDone(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject loadedPrefab = obj.Result;
            GameObject pref = Object.Instantiate(loadedPrefab, _spawnPosition);
            _stopMenu = pref.GetComponent<StopMenu>();
            _panelIsCreated = true;
        }
    }
}