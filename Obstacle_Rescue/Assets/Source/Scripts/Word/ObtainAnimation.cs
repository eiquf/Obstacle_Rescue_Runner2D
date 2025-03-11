using DG.Tweening;
using UnityEngine;

public class ObtainAnimation : IAnimation
{
    private readonly float _moveDuration = 0.5f;
    private readonly float _heightOffset = -1.5f;
    private readonly float _waitBeforeFade = 2.5f;
    private readonly float _fadeDuration = 0.5f;

    public void PlayAnimation(Transform transform)
    {
        Camera cam = Camera.main;
        Vector3 screenTopCenter = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f, cam.transform.position.z * -1));
        screenTopCenter.y += _heightOffset;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(screenTopCenter, _moveDuration).SetEase(Ease.OutBack));
        sequence.AppendInterval(_waitBeforeFade);
        sequence.Append(transform.DOScale(Vector3.zero, _fadeDuration).SetEase(Ease.InBack))
                .OnComplete(() => Object.Destroy(transform.gameObject));
    }
}