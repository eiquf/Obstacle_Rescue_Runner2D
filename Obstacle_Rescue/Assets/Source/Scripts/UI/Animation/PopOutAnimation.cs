using DG.Tweening;
using UnityEngine;

public class PopOutAnimation : IAnimation
{
    private readonly RectTransform _panelRect;
    private readonly CanvasGroup _canvasGroup;
    private readonly float _fadeInDuration;
    private readonly float _popOutDuration;
    private readonly bool _fromTheLeft;

    public PopOutAnimation(RectTransform panelRect, CanvasGroup canvasGroup, float fadeInDuration, float popOutDuration, bool fromTheLeft)
    {
        _panelRect = panelRect;
        _canvasGroup = canvasGroup;
        _fadeInDuration = fadeInDuration;
        _popOutDuration = popOutDuration;
        _fromTheLeft = fromTheLeft;
    }

    public void PlayAnimation(Transform transform)
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.DOFade(1, _fadeInDuration);

        Vector2 startPos = _fromTheLeft ? new Vector2(-Screen.width, 0f) : new Vector2(Screen.width, 0f);
        Vector2 targetPos = Vector2.zero;

        _panelRect.anchoredPosition = startPos;

        DOTween.Sequence()
            .Append(_panelRect.DOAnchorPos(targetPos, _popOutDuration).SetEase(Ease.OutBack));
    }
}
