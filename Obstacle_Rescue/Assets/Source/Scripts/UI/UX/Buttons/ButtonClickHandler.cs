using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class ButtonClickHandler : IMenu
{
    private Button[] _defaultButtons;
    private ButtonInitializer _buttonInitializer;
    private readonly Transform _buttonsPanelPos;

    private readonly ButtonActionsFactory _buttonActionsFactory = new();
    private readonly AnimationContext _animationContext = new();
    private readonly SoundController _soundController;

    private readonly string _sceneName;
    public ButtonClickHandler(Transform buttonsPanelPos, Transform[] createPos, InjectContainer container)
    {
        _buttonsPanelPos = buttonsPanelPos;
        _soundController = container.SoundController;

        _buttonActionsFactory.SetComponents(container, createPos);
        _sceneName = SceneManager.GetActiveScene().name;
    }

    public void Execute()
    {
        _buttonInitializer = new ButtonInitializer(_buttonsPanelPos, OnButtonClick);
        _defaultButtons = _buttonInitializer.Execute();
    }

    private void OnButtonClick(int index)
    {
        IButtonAction action = _buttonActionsFactory.GetMenuButtonAction(index, _sceneName);
        action?.Execute();

        ButtonsTapAnimation(_defaultButtons[index].transform);
        _soundController.IsSoundPlay?.Invoke(index);
    }
    private void ButtonsTapAnimation(Transform transform)
    {
        _animationContext.SetAnimationStrategy(new ButtonTapAnimation());
        _animationContext.PlayAnimation(transform);
    }
}