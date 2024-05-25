using DG.Tweening;
using System;
using System.Linq;
using UnityEngine;

public class PreferencesAnimation : IPreferencesAnimation
{
    private RectTransform[] _buttonsRectTransforms;

    private readonly float _animationDuration = 0.4f;
    private readonly float _buttonSpacing = 5f;

    public PreferencesAnimation(RectTransform[] rectTransform) => _buttonsRectTransforms = rectTransform;

    public void Execute(bool isUpsideDown, bool isPanelOn)
    {
        float offsetY;
        bool isClose = false;
        Action<RectTransform, int> animationAction = (buttonRect, i) =>
        {
            if (isPanelOn)
            {
                offsetY = isUpsideDown ? -50 - i * (buttonRect.rect.height + _buttonSpacing) : 50 + i * (buttonRect.rect.height + _buttonSpacing);
                isClose = false;
            }
            else
            {
                offsetY = 0;
                isClose = true;
            }

            if (!isClose)
            {
                if (buttonRect.gameObject.activeSelf == false)
                    buttonRect.gameObject.SetActive(true);
                buttonRect.DOScale(1f, 0.5f).SetEase(Ease.OutCirc, 0.5f);
            }
            else buttonRect.DOScale(0f, 0.3f).SetEase(Ease.InCirc, 0.5f).OnComplete(() => buttonRect.gameObject.SetActive(false));

            Vector2 targetPosition = new(buttonRect.anchoredPosition.x, offsetY);

            buttonRect.DOAnchorPos(targetPosition, _animationDuration)
                .SetEase(Ease.OutBack)
                .SetDelay(i * 0.1f);
        };

        if (_buttonsRectTransforms != null)
        {
            foreach (var (buttonRect, index) in _buttonsRectTransforms.Select((b, i) => (b, i)).Reverse())
                animationAction(buttonRect, index);
        }
    }
}