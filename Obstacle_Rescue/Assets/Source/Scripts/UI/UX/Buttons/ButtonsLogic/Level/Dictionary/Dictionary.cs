using UnityEngine;
using UnityEngine.UI;
public class Dictionary : IButtonAction
{
    private readonly PanelToggler _panelToggler;
    private readonly AlternateCounter _counter = new();
    private readonly Text _amountText;
    private readonly Button _button;

    public Dictionary(Transform panel, Transform buttonPos)
    {
        _panelToggler = new PanelToggler(panel);
        _amountText = buttonPos.GetComponentInChildren<Text>();
        _button = buttonPos.GetComponent<Button>();
    }

    public void Execute()
    {
        _panelToggler.Toggle();
        _amountText.text = _counter.GetText();

        if (_counter.GetCurrent() <= 0)
            _button.interactable = false;

        EventBus.RaiseGameStopped(_panelToggler.IsActive());
    }
}
