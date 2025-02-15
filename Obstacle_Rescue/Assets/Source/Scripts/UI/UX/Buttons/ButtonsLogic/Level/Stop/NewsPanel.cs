using UnityEngine;
using UnityEngine.UI;

public class NewsPanel
{
    private readonly Transform _panel;
    private readonly Transform _eiquif;

    private readonly Transform _buttonContainer;
    private readonly Button[] _buttons;
    private readonly ButtonsActions _buttonActions;

    private readonly CanvasGroup _canvasGroup;
    private readonly AnimationContext _eiquifAnim = new();
    private readonly AnimationContext _animation = new();
    private readonly bool _isFromTheLeft = false;

    public NewsPanel(Transform panel, Transform eiquif)
    {
        _panel = panel;
        _buttonContainer = panel.GetChild(3);
        _canvasGroup = _panel.GetComponent<CanvasGroup>();
        _animation.SetAnimationStrategy(new PopOutAnimation(_isFromTheLeft, _canvasGroup));
        _eiquifAnim.SetAnimationStrategy(new PopOutAnimation(!_isFromTheLeft));
        _buttonActions = new ButtonsActions();
        _buttons = InitializeButtons();
        _eiquif = eiquif;

    }

    private Button[] InitializeButtons() => new ButtonInitializer(_buttonContainer, OnButtonClick).Execute();
    public void Toggle(bool isVisible)
    {
        _panel.gameObject.SetActive(isVisible);
        if (isVisible)
        {
            _animation.PlayAnimation(_panel);
            _eiquifAnim.PlayAnimation(_eiquif);
        }
    }

    private void OnButtonClick(int index)
    {
        IButtonAction action = _buttonActions.GetSMButtonAction(index);
        action?.Execute(); 
        _animation.SetAnimationStrategy(new ButtonTapAnimation());
        _animation.PlayAnimation(_buttons[index].transform);
    }
}