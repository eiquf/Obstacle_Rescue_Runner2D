using DG.Tweening;
using UnityEngine;

public class PropUIAnimation : IAnimation
{
    private readonly float _endValue, _duration, _overshoot = 10f;
    private readonly Ease _ease;

    public PropUIAnimation(bool isEnable)
    {
        (_endValue, _duration, _ease) = isEnable
            ? (1f, 0.3f, Ease.OutBack)
            : (0f, 0.2f, Ease.InBack);
    }

    public void OnDisable(Transform transform) => transform.DOScale(0f, 0.2f).SetEase(Ease.InBack, _overshoot);
    public void PlayAnimation(Transform transform) => transform.DOScale(_endValue, _duration).SetEase(_ease, _overshoot);
}