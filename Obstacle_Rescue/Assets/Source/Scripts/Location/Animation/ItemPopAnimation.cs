using DG.Tweening;
using UnityEngine;

public class ItemPopAnimation : IAnimation
{
    public float popUpScale = 1.2f;
    public float animationDuration = 0.1f;
    public float fadeDuration = 0.1f;

    private Vector3 originalScale;
    public void PlayAnimation(Transform transform)
    {
        originalScale = transform.localScale;

        transform.localScale = Vector3.zero;

        Sequence popUpSequence = DOTween.Sequence();

        popUpSequence.Append(transform.DOScale(popUpScale, animationDuration / 2).SetEase(Ease.OutBack));
        popUpSequence.Append(transform.DOScale(originalScale, animationDuration / 2).SetEase(Ease.InBack));

        popUpSequence.OnComplete(() => Object.Destroy(transform.gameObject));
        popUpSequence.Play();
    }
}