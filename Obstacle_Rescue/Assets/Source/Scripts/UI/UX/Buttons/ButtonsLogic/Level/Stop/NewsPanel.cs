using System;
using UnityEngine;
using UnityEngine.UI;

public class NewsPanel : MonoBehaviour
{
    public Action<bool> IsActivated;

    [SerializeField] private float popOutDuration = 1f;
    [SerializeField] private float popOutDistance = 200f;
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private bool _isFromTheLeft = false;
    private Transform SmPos => transform.GetChild(3);

    private ButtonActionsFactory _buttonActionsFactory = new();
    private ButtonInitializer _buttonInitializer;
    private Button[] _smButtons;

    private PopOutAnimation _panelAnimator;
    private void Awake()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        _panelAnimator = new PopOutAnimation
            (rectTransform, 
            canvasGroup, 
            fadeInDuration, 
            popOutDuration, 
            _isFromTheLeft);

        IsActivated += _panelAnimator.PlayAnimation;
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
    private void ButtonsTapAnimation(Transform transform) => new ButtonTapAnimation().Execute(transform);
    private void OnEnable() => IsActivated += _panelAnimator.PlayAnimation;
    private void OnDisable() => IsActivated -= _panelAnimator.PlayAnimation;
}