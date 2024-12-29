using System.Collections.Generic;
using UnityEngine;

public class RemoveHealth : HealthSystem
{
    private readonly Death _dead;
    private readonly AnimationContext _animator = new();

    public RemoveHealth(LivesSettings livesSettings, List<GameObject> hurts, Death dead) : base(livesSettings, hurts) => _dead = dead;
    public override void Execute()
    {
        if (_hurts.Count <= _livesSettings.MaxLives && _hurts.Count != _livesSettings.MinLives)
        {
            Object.Destroy(_hurts[0], 0.25f);

            _animator.SetAnimationStrategy(new PropUIAnimation(false));
            _animator.PlayAnimation(_hurts[0].transform);

            _hurts.Remove(_hurts[0]);
        }
        else _dead.Execute();
    }
}