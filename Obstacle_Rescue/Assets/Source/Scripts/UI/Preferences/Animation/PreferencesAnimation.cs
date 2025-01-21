using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;

public sealed class PreferencesAnimation : IAnimation
{
    private readonly SceneChecker _sceneChecker = new();
    private readonly RectTransform[] _buttonsRectTransforms;

    private readonly bool _isUpsideDown;
    private const float AnimationDuration = 0.4f;
    private const float ButtonSpacing = 5f;

    private bool _isPanelOn = true;
    public PreferencesAnimation(Transform preferencesCreatePos)
    {
        _buttonsRectTransforms = preferencesCreatePos.GetComponentsInChildren<RectTransform>();
        _sceneChecker.Execute();
        _isUpsideDown = _sceneChecker.CurrentScene.name != "Menu";
        _isPanelOn = false;
    }
    public void PlayAnimation(Transform transform)
    {
        if (_buttonsRectTransforms == null) return;

        foreach (var (buttonRect, index) in _buttonsRectTransforms.Select((b, i) => (b, i)).Reverse())
        {
            float offsetY = _isPanelOn == true ?
                (_isUpsideDown ? -50 - index * (buttonRect.rect.height + ButtonSpacing) : 50 + index * (buttonRect.rect.height + ButtonSpacing)): 0;

            if (_isPanelOn == true)
            {
                if (!buttonRect.gameObject.activeSelf)
                    buttonRect.gameObject.SetActive(true);

                buttonRect.DOScale(1f, 0.5f).SetEase(Ease.OutCirc);
            }
            else
            {
                buttonRect.DOScale(0f, 0.3f).SetEase(Ease.InCirc)
                    .OnComplete(() => buttonRect.gameObject.SetActive(false));
            }

            buttonRect.DOAnchorPos(new Vector2(buttonRect.anchoredPosition.x, offsetY), AnimationDuration)
            .SetEase(Ease.OutBack)
            .SetDelay(index * 0.15f);
        }
        _isPanelOn = !_isPanelOn;
    }
}