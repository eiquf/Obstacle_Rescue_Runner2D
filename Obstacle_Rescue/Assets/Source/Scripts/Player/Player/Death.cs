using System.Collections.Generic;
using UnityEngine;

public sealed class Death : HealthSystem
{
    private readonly GameCamera _camera;
    private readonly Player _player;

    public Death(List<GameObject> hurts, GameCamera camera, Player player, LivesSettings livesSettings = null) : base(livesSettings, hurts)
    {
        _camera = camera;
        _player = player;
    }
    public override void Execute()
    {
        _player.IsStop?.Invoke(true);
        _hurts.Clear();
        _camera.IsZoomed?.Invoke(true);
    }
}