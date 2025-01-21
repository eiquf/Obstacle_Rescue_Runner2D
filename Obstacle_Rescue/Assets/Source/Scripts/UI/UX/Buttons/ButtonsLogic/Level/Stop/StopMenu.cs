using System;
using UnityEngine;

public class StopMenu : MonoBehaviour
{
    public Action<bool> IsPanelActivated { get; set; }
    private NewsPanel _newsPanel;
    private void OnEnable() => IsPanelActivated += ActivatePanel;
    private void OnDisable() => IsPanelActivated -= ActivatePanel;
    private void Start() => _newsPanel = transform.GetChild(1).GetComponent<NewsPanel>();
    private void ActivatePanel(bool activate)
    {
        switch (activate)
        {
            case true: Active(); break;
            case false: Deactivate(); break;
        }
    }
    private void Active() => _newsPanel.gameObject.SetActive(true);
    private void Deactivate()
    {
        _newsPanel.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}