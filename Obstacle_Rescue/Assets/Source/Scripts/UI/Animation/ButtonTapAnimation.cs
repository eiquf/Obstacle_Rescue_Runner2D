using DG.Tweening;
using UnityEngine;

public class ButtonTapAnimation : IAnimation
{
    private readonly Vector3 _pressedScale = new(0.9f, 0.9f, 0.9f);
    private readonly float _animationDuration = 0.1f;

    public void PlayAnimation(Transform transform)
    {
        transform.DOScale(_pressedScale, _animationDuration)
            .SetEase(Ease.OutCirc)
            .OnComplete(() => transform.DOScale(Vector3.one, _animationDuration).SetEase(Ease.InOutCirc));
    }
}