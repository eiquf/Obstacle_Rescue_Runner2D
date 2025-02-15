using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : IMenu
{
    private readonly Transform[] _createPos;
    private Button[] _defaultButtons;
    private readonly Transform _buttonsPanelPos;

    private readonly ButtonsActions _buttonActionsFactory = new();
    private readonly AnimationContext _animationContext = new();
    private readonly SceneChecker _sceneChecker = new();

    private readonly InjectContainer _container;
    public ButtonClickHandler(Transform buttonsPanelPos, Transform[] createPos, InjectContainer container)
    {
        _buttonsPanelPos = buttonsPanelPos;
        _container = container;
        _createPos = createPos;
    }
    public void Execute()
    {
        _sceneChecker.Execute();
        _defaultButtons = new ButtonInitializer(_buttonsPanelPos, OnButtonClick).Execute();
        _animationContext.SetAnimationStrategy(new ButtonTapAnimation());
    }
    private void OnButtonClick(int index)
    {
        IButtonAction action = _buttonActionsFactory.GetMenuButtonAction(index, _createPos, _container);
        action?.Execute();

        ButtonsTapAnimation(_defaultButtons[index].transform);
        _container.SoundController.IsSoundPlay?.Invoke(index);
    }
    private void ButtonsTapAnimation(Transform transform) => _animationContext.PlayAnimation(transform);
}