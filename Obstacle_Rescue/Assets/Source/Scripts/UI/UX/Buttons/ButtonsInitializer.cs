using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInitializer : IComponentsInitialize<Button>
{
    private readonly Transform _buttonsPanelPos;
    private Action<int> _onButtonClick;

    public ButtonInitializer(Transform buttonsPanelPos, Action<int> onButtonClick)
    {
        _buttonsPanelPos = buttonsPanelPos;
        _onButtonClick = onButtonClick;
    }

    public Button[] Execute()
    {
        int buttonCount = _buttonsPanelPos.childCount;
        Button[] buttons = new Button[buttonCount];
        for (int i = 0; i < buttonCount; i++)
        {
            int index = i;
            buttons[i] = _buttonsPanelPos.GetChild(i).GetComponent<Button>();
            buttons[i].onClick.AddListener(() => _onButtonClick(index));
        }
        return buttons;
    }
}