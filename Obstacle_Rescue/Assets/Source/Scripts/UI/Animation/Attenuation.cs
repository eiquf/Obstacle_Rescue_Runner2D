using DG.Tweening;
using UnityEngine;

public class Attenuation : MonoBehaviour
{
    private readonly float _fadeDuration = 0.8f;
    private CanvasGroup _attenuationImage;

    private SceneChecker _sceneChecker = new();
    private void Start()
    {
        _attenuationImage = GetComponent<CanvasGroup>();
        Activate();
    }
    private void FixedUpdate() => _sceneChecker.Execute();
    private void OnEnable() => _sceneChecker.OnNotify += Activate;
    private void OnDisable() => _sceneChecker.OnNotify -= Activate;
    private void Activate()
    {
        gameObject.SetActive(true);
        _attenuationImage.alpha = 1;

        _attenuationImage.DOFade(0f, _fadeDuration)
           .OnComplete(() => Deactivate());
    }
    private void Deactivate()
    {
        _attenuationImage.alpha = 1;
        gameObject.SetActive(false);
    }
}