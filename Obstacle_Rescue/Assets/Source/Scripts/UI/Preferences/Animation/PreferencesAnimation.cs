using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;

public sealed class PreferencesAnimation : IAnimation
{
    private readonly RectTransform[] _buttonsRectTransforms;
    private readonly bool _isUpsideDown;

    private const float AnimationDuration = 0.4f;
    private const float ButtonSpacing = 5f;
    private const float ScaleDuration = 0.5f;
    private const float ScaleHideDuration = 0.3f;
    private const float DelayBetweenButtons = 0.15f;

    private bool _isPanelOn;
    private readonly SceneChecker _sceneChecker = new();
    public PreferencesAnimation(Transform preferencesCreatePos)
    {
        _sceneChecker.Execute();

        _buttonsRectTransforms = preferencesCreatePos.GetComponentsInChildren<RectTransform>();
        _isUpsideDown = _sceneChecker.CurrentScene.name != "Menu";
        _isPanelOn = false;
    }

    public void PlayAnimation(Transform transform)
    {
        if (_buttonsRectTransforms == null || _buttonsRectTransforms.Length == 0) return;

        _isPanelOn = !_isPanelOn;
        float directionMultiplier = _isUpsideDown ? -1 : 1;

        foreach (var (button, index) in _buttonsRectTransforms.Select((b, i) => (b, i)).Reverse())
        {
            AnimateButton(button, index, directionMultiplier);
        }
    }

    private void AnimateButton(RectTransform button, int index, float directionMultiplier)
    {
        float offsetY = _isPanelOn
            ? directionMultiplier * (50 + index * (button.rect.height + ButtonSpacing))
            : 0;

        if (_isPanelOn)
        {
            button.gameObject.SetActive(true);
            button.DOScale(1f, ScaleDuration).SetEase(Ease.OutCirc);
        }
        else
        {
            button.DOScale(0f, ScaleHideDuration).SetEase(Ease.InCirc)
                .OnComplete(() => button.gameObject.SetActive(false));
        }

        button.DOAnchorPos(new Vector2(button.anchoredPosition.x, offsetY), AnimationDuration)
            .SetEase(Ease.OutBack)
            .SetDelay(index * DelayBetweenButtons);
    }
}