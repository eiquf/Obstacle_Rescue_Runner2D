using System;
using UnityEngine;

public class StopMenu : MonoBehaviour
{
    public Action<bool> IsPanelActivated { get; set; }

    private const bool _isDownUpside = true;
    private const bool _canSpawnHome = true;
    private Transform PreferencesCreatePos => transform.GetChild(0);
    private NewsPanel _newsPanel;

    private InjectContainer _container;
    private Preferences _preferences;

    private void OnEnable() => IsPanelActivated += ActivatePanel;
    private void OnDisable() => IsPanelActivated -= ActivatePanel;
    private void Start()
    {
        _container = FindFirstObjectByType<InjectContainer>();
        _newsPanel = transform.GetChild(1).GetComponent<NewsPanel>();

        _preferences = new(_isDownUpside, _container, _canSpawnHome);
    }
    private void ActivatePanel(bool activate)
    {
        switch (activate)
        {
            case true: Active(); break;
            case false: Deactivate(); break;
        }
    }
    private void Active()
    {
        _newsPanel.IsActivated?.Invoke(true);
        _preferences.Execute(PreferencesCreatePos);
    }
    private void Deactivate()
    {
        _newsPanel.IsActivated?.Invoke(false);
        _preferences.Execute(PreferencesCreatePos);
        gameObject.SetActive(false);
    }
}