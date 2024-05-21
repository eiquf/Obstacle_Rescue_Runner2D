using DG.Tweening;
using UnityEngine;

public class ButtonTapAnimation
{
    private Vector3 _pressedScale = new Vector3(0.9f, 0.9f, 0.9f);
    private float _animationDuration = 0.1f;

    private Transform _transform;
    public void Execute(Transform transform)
    {
        _transform = transform;

        transform.DOScale(_pressedScale, _animationDuration)
           .SetEase(Ease.OutCirc)
           .OnComplete(() => ResetButtonScale());
    }
    private void ResetButtonScale()
    {
        _transform.DOScale(Vector3.one, _animationDuration)
            .SetEase(Ease.InOutCirc);
    }
}