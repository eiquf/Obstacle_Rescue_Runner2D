using UnityEngine;

public sealed class Preferences : IButtonAction
{
    private readonly AnimationContext _animation = new();
    private readonly Transform _spawnPos;
    private bool _buttonsActivated = false;

    public Preferences(Transform spawnPos)
    {
        _buttonsActivated = false;
        _spawnPos = spawnPos;
        _animation.SetAnimationStrategy(new PreferencesAnimation(_spawnPos));
    }

    public void Execute()
    {
        _buttonsActivated = !_buttonsActivated;
        _animation.PlayAnimation(null);
    }
}