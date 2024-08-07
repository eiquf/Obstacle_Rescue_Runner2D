using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerDead : Health
{
    private readonly MainCameraFactory _mainCameraFactory;
    private readonly PlayerAnimation _animation;

    public PlayerDead
        (LivesSettings livesSettings, 
        List<GameObject> hurts,
        PlayerAnimation animation,
        MainCameraFactory factory) : base(livesSettings, hurts)
    {
        _animation = animation;
        _mainCameraFactory = factory;
    }

    public override void Execute()
    {
        _hurts.Clear();
        _mainCameraFactory.IsZoomed?.Invoke(true);
        _animation.PlayerIsUpset?.Invoke();
    }
}