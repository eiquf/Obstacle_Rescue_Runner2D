using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInitializer : IComponentsInitialize<Button>
{
    private readonly Transform _buttonsPanelPos;
    private readonly Action<int> _onButtonClick;
    private readonly Action _onPlaySound;

    public ButtonInitializer(Transform buttonsPanelPos, Action<int> onButtonClick, Action soundPlay)
    {
        _buttonsPanelPos = buttonsPanelPos;
        _onButtonClick = onButtonClick;
        _onPlaySound = soundPlay;
    }

    public Button[] Execute()
    {
        int buttonCount = _buttonsPanelPos.childCount;
        Button[] buttons = new Button[buttonCount];
        for (int i = 0; i < buttonCount; i++)
        {
            buttons[i] = _buttonsPanelPos.GetChild(i).GetComponent<Button>();
        }

        for (int i = 0; i < buttonCount; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => 
            {
                _onButtonClick(index); 
                _onPlaySound();
            });
        }

        return buttons;
    }
}