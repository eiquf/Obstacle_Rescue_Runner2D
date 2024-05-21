using DG.Tweening;
using UnityEngine;

public class Attenuation : MonoBehaviour
{
    private readonly float _fadeDuration = 0.8f;
    private CanvasGroup _attenuationImage;
    private void OnEnable()
    {
        _attenuationImage = GetComponent<CanvasGroup>();
        _attenuationImage.alpha = 1;

        _attenuationImage.DOFade(0f, _fadeDuration)
           .OnComplete(() => Destroy(gameObject));
    }
}