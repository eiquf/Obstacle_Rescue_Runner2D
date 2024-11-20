using UnityEngine;

public class AnimationFactory
{
    public PopOutAnimation CreatePopOutAnimation(
        RectTransform panelRect,
        CanvasGroup canvasGroup,
        float fadeInDuration,
        float popOutDuration,
        bool fromTheLeft) =>
    new(panelRect, canvasGroup, fadeInDuration, popOutDuration, fromTheLeft);

    //public Attenuation CreateAttenuation(CanvasGroup canvasGroup, SceneChecker sceneChecker, float fadeDuration = 0.8f) =>
    //    new(canvasGroup, sceneChecker, fadeDuration);

    //public ButtonTapAnimation CreateButtonTapAnimation(float animationDuration = 0.1f) =>
    //    new(animationDuration);
}
public interface IUIAnimation
{
    void PlayAnimation();
}