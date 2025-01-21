using UnityEngine;
using UnityEngine.UI;

public class NewsPanel : MonoBehaviour
{
    private readonly bool _isFromTheLeft = false;
    [SerializeField] private Transform EiquifPos;
    private Transform SmPos => transform.GetChild(3);

    private readonly AnimationContext _animationContext = new();
    private readonly AnimationContext _animationContextEiquif = new();

    private readonly ButtonsActions _buttonActionsFactory = new();
    private CanvasGroup _canvasGroup;
    private ButtonInitializer _buttonInitializer;
    private Button[] _smButtons;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        _animationContext.SetAnimationStrategy(new PopOutAnimation(_isFromTheLeft, _canvasGroup));
        _animationContextEiquif.SetAnimationStrategy(new PopOutAnimation(!_isFromTheLeft));

        _buttonInitializer = new ButtonInitializer(SmPos, OnButtonClick);
        _smButtons = _buttonInitializer.Execute();
    }
    private void OnButtonClick(int index)
    {
        IButtonAction action = _buttonActionsFactory.GetSMButtonAction(index);
        action?.Execute();

        ButtonsTapAnimation(_smButtons[index].transform);
    }
    private void ButtonsTapAnimation(Transform transform)
    {
        _animationContext.SetAnimationStrategy(new ButtonTapAnimation());
        _animationContext.PlayAnimation(transform);
    }
    private void OnEnable()
    {
        _animationContext.PlayAnimation(transform);
        _animationContextEiquif.PlayAnimation(EiquifPos);
    }
}