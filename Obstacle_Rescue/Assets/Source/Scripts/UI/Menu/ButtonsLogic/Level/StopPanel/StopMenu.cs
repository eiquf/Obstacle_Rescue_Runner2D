using System;
using System.Collections;
using UnityEngine;

public class StopMenu : MonoBehaviour
{
    public Action<bool> IsPanelActivated { get; set; }

    private const bool _isDownUpside = true;
    #region Time
    private const float DelayBeforeDeactivate = 0.5f;
    #endregion
    private Transform _preferencesCreatePos => transform.GetChild(0);
    private NewsPanel _newsPanel;

    private void OnEnable() => IsPanelActivated += ActivatePanel;
    private void OnDisable() => IsPanelActivated -= ActivatePanel;
    private void Start()
    {
        //_injector = FindFirstObjectByType<UIInjector>();
        _newsPanel = transform.GetChild(1).GetComponent<NewsPanel>();
        //Preferences();
    }
    private void ActivatePanel(bool activate)
    {
        switch (activate)
        {
            case true: Active(); break;
            case false: StartCoroutine(Deactivate()); break;
        }
    }
    private void Active()
    {
        _newsPanel.IsActivated?.Invoke(true);
        //_preferencesSettings.Execute();
    }
    private IEnumerator Deactivate()
    {
        _newsPanel.IsActivated?.Invoke(false);
        //_preferencesSettings.Execute();
        yield return new WaitForSeconds(DelayBeforeDeactivate);
        gameObject.SetActive(false);
    }
}