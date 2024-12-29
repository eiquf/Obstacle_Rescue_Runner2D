using DG.Tweening;
using UnityEngine;

public class Attenuation : IAnimation
{
    private readonly float _fadeDuration;

    public Attenuation(float fadeDuration) => _fadeDuration = fadeDuration;

    public void PlayAnimation(Transform transform)
    {
        var canvasGroup = transform.GetComponent<CanvasGroup>();
        if (canvasGroup == null) return;

        canvasGroup.alpha = 1;
        canvasGroup.DOFade(0f, _fadeDuration)
            .OnComplete(() => transform.gameObject.SetActive(false));
    }
}