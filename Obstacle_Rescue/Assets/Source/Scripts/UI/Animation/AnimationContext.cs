using UnityEngine;

public class AnimationContext
{
    private IUIAnimation _animationStrategy;

    public void SetAnimationStrategy(IUIAnimation strategy) => _animationStrategy = strategy;

    public void PlayAnimation(Transform transform) => _animationStrategy?.PlayAnimation(transform);
}
public interface IUIAnimation
{
    void PlayAnimation(Transform transform);
}