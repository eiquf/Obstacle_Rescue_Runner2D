using UnityEngine;
using UnityEngine.AddressableAssets;

public class StopMenuPanelActivator : IUIPanelsInstantiate
{
    private const string _panelName = "StopPanel";

    private bool _gameIsStopped = false;
    private bool _panelIsCreated = false;

    private StopMenu _stopMenu;

    public void Execute(Transform spawn)
    {
        if (!_gameIsStopped)
        {
            if (!_panelIsCreated) CreatePanel(spawn);
            else
            {
                _stopMenu.gameObject.SetActive(true);
                _stopMenu.IsPanelActivated?.Invoke(true);
            }
        }
        else _stopMenu.IsPanelActivated?.Invoke(false);

        _gameIsStopped = !_gameIsStopped;
    }
    private void CreatePanel(Transform spawn)
    {
        Addressables.InstantiateAsync(_panelName, spawn);
        _panelIsCreated = true;
    }
}