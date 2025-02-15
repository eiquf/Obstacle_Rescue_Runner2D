using UnityEngine;

public class PanelToggler
{
    private readonly Transform _panel;
    public PanelToggler(Transform panel) => _panel = panel;
    public void Toggle() => _panel.gameObject.SetActive(!_panel.gameObject.activeSelf);
}
