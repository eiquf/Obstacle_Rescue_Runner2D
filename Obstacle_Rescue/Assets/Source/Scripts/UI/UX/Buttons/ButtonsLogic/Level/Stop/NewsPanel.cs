using UnityEngine;
using UnityEngine.UI;

public class NewsPanel : MonoBehaviour
{
    [SerializeField] private float popOutDuration = 1f;
    [SerializeField] private float popOutDistance = 200f;
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private bool _isFromTheLeft = false;
    private Transform SmPos => transform.GetChild(3);

    private ButtonActionsFactory _buttonActionsFactory = new();
    private ButtonInitializer _buttonInitializer;
    private Button[] _smButtons;

    private readonly AnimationContext _animationContext = new();
    private void Awake()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        _animationContext.SetAnimationStrategy(new PopOutAnimation
            (rectTransform,
            canvasGroup,
            fadeInDuration,
            popOutDuration,
            _isFromTheLeft));

        _animationContext.PlayAnimation(transform);
    }

    private void Start()
    {
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
    private void OnEnable() => _animationContext.PlayAnimation(transform);
    private void OnDisable() => _animationContext.PlayAnimation(transform);
}