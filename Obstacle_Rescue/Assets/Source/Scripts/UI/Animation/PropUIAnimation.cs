using DG.Tweening;
using UnityEngine;

public class PropUIAnimation
{
    public void OnEnable(Transform transform) => transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack, 10f);
    public void OnDisable(Transform transform) => transform.DOScale(0f, 0.2f).SetEase(Ease.InBack, 10f);
}