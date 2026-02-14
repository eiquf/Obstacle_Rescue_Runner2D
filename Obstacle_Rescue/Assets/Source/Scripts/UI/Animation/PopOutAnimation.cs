using DG.Tweening;
using UnityEngine;

public class PopOutAnimation : IAnimation
{
    private readonly CanvasGroup _canvasGroup;
    private readonly float _fadeInDuration = 1f;
    private readonly float _popOutDuration = 0.5f;
    private readonly bool _fromTheLeft;
    private Vector2 _targetPos;
    private bool _isSetPos = false;

    public PopOutAnimation(bool fromTheLeft, CanvasGroup canvasGroup = null)
    {
        _canvasGroup = canvasGroup;
        _fromTheLeft = fromTheLeft;
    }

    public void PlayAnimation(Transform transform)
    {
        if (_canvasGroup != null)
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, _fadeInDuration);
        }

        Vector2 startPos = _fromTheLeft ? new Vector2(-Screen.width, 0f) : new Vector2(Screen.width, 0f);
        if (_isSetPos == false)
        {
            _targetPos = transform.position;
            _isSetPos = true;
        }

        transform.position = startPos;
        DOTween.Sequence()
            .Append(transform.DOMove(_targetPos, _popOutDuration).SetEase(Ease.OutBack));
    }
}
