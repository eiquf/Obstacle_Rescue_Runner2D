using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public sealed class EducationPanelSpawn : IButtonAction
{
    private const string _educPanelName = "EducationPanel";

    private readonly Transform _spawnPosition;
    private bool _isActivated = false;
    private GameObject _educationPanelPref;
    public EducationPanelSpawn(Transform spawnPosition) => _spawnPosition = spawnPosition;
    public void Execute()
    {
        try
        {
            if (_isActivated == false) CreatePanel(_spawnPosition);
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