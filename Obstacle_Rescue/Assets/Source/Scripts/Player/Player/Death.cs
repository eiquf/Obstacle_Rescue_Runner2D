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
        if (_player.transform.position.y < -20f)
            Object.Destroy(_player.gameObject);
        else
            _player.IsStop?.Invoke(true);

        _hurts.ForEach(hurt => { if (hurt != null) Object.Destroy(hurt); });
        _hurts.Clear();
        _camera.IsZoomed?.Invoke(true);
    }
}