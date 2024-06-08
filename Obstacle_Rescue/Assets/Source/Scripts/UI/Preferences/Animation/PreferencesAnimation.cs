using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;

public class PreferencesAnimation
{
    private readonly RectTransform[] _buttonsRectTransforms;
    private readonly bool _isUpsideDown;
    private readonly bool _isPanelOn;
    private const float AnimationDuration = 0.4f;
    private const float ButtonSpacing = 5f;

    public PreferencesAnimation(RectTransform[] rectTransforms, bool isUpsideDown, bool isPanelOn)
    {
        _buttonsRectTransforms = rectTransforms;
        _isUpsideDown = isUpsideDown;
        _isPanelOn = isPanelOn;
    }

    public void Execute()
    {
        if (_buttonsRectTransforms == null) return;

        foreach (var (buttonRect, index) in _buttonsRectTransforms.Select((b, i) => (b, i)).Reverse())
        {
            float offsetY = _isPanelOn ?
                (_isUpsideDown ? -50 - index * (buttonRect.rect.height + ButtonSpacing) : 50 + index * (buttonRect.rect.height + ButtonSpacing))
                : 0;

            if (_isPanelOn)
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
                .SetDelay(index * 0.1f);
        }
    }
}