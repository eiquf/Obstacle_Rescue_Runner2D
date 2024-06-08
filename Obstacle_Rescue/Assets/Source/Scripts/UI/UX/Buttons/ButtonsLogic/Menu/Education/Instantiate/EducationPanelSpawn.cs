using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public sealed class EducationPanelSpawn : IUIPanelsInstantiate
{
    private const string _educPanelName = "EducationPanel";

    private bool _isActivated = false;
    private GameObject _educationPanelPref;
    public void Execute(Transform spawnPos)
    {
        try
        {
            if (_isActivated == false) CreatePanel(spawnPos);
            else DestroyPanel();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error while Education Panel was instantiating: {ex}");
        }
    }
    private void CreatePanel(Transform spawnPos)
    {
        Addressables.InstantiateAsync(_educPanelName, spawnPos).Completed += handle => _educationPanelPref = handle.Result;
        _isActivated = true;
    }
    private void DestroyPanel()
    {
        UnityEngine.Object.Destroy(_educationPanelPref);
        _isActivated = false;
    }
}