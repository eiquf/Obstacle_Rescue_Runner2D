using System.Collections.Generic;
using UnityEngine;

public sealed class AddHealth : HealthSystem
{
    private readonly HealthSystemPrefabsLoader _prefabLoader = new();

    private readonly AnimationContext _animator = new();
    private readonly Transform _spawn;

    public AddHealth(LivesSettings livesSettings, List<GameObject> hurts, Transform spwan) : base(livesSettings, hurts) => _spawn = spwan;
    public override void Execute()
    {
        if (_hurts.Count < _livesSettings.MaxLives && _hurts.Count != _livesSettings.MinLives)
        {
            var hurtPref = Object.Instantiate(_prefabLoader.Execute(), _spawn);
            _animator.SetAnimationStrategy(new PropUIAnimation(true));
            _animator.PlayAnimation(hurtPref.transform);
            _hurts.Add(hurtPref);
        }
    }
}