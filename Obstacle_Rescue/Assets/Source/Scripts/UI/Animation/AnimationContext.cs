using UnityEngine;

public class AnimationContext
{
    private IAnimation _animationStrategy;

    public void SetAnimationStrategy(IAnimation strategy) => _animationStrategy = strategy;

    public void PlayAnimation(Transform transform) => _animationStrategy?.PlayAnimation(transform);
}
public interface IAnimation
{
    void PlayAnimation(Transform transform);
}