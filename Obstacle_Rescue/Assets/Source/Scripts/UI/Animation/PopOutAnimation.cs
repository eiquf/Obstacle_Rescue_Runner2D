using DG.Tweening;
using UnityEngine;

public class PopOutAnimation
{
    private readonly RectTransform _panelRect;
    private readonly CanvasGroup _canvasGroup;
    private readonly float _fadeInDuration;
    private readonly float _popOutDuration;
    private readonly bool _fromTheLeft;

    private Vector2 _startPos;
    private Vector2 _targetPos;

    public PopOutAnimation
        (RectTransform panelRect,
        CanvasGroup canvasGroup,
        float fadeInDuration,
        float popOutDuration,
        bool fromTheLeft)
    {
        _panelRect = panelRect;
        _canvasGroup = canvasGroup;
        _fadeInDuration = fadeInDuration;
        _popOutDuration = popOutDuration;
        _fromTheLeft = fromTheLeft;
    }

    public void PlayAnimation(bool activation)
    {
        _canvasGroup.alpha = activation ? 0 : 1;
        float endValue = activation ? 1 : 0;
        _canvasGroup.DOFade(endValue, _fadeInDuration);

        if (_fromTheLeft == true)
        {
            _startPos = activation ? new Vector2(-Screen.width, 0f) : Vector2.zero;
            _targetPos = activation ? Vector2.zero : new Vector2(-Screen.width, 0f);
        }
        else
        {
            _startPos = activation ? new Vector2(Screen.width, 0f) : Vector2.zero;
            _targetPos = activation ? Vector2.zero : new Vector2(Screen.width, 0f);
        }

        _panelRect.anchoredPosition = _startPos;



        DOTween.Sequence()
            .Append(_panelRect.DOJump(_targetPos, 1f, 1, _popOutDuration))
            .Join(_panelRect.DOAnchorPos(_targetPos, _popOutDuration).SetEase(Ease.OutBack));
    }
}