using UnityEngine;

public class RemoveHealth : Health
{
    private readonly PlayerDeath _playerDeath = new();
    public override void Execute()
    {
        if (hurts.Count <= _livesSettings.MaxLives && hurts.Count != _livesSettings.MinLives)
        {
            Object.Destroy(hurts[0], 0.25f);
            _propUIAnim.OnDisable(hurts[0].transform);
            hurts.Remove(hurts[0]);
        }
        else
            _playerDeath.Execute();
    }
}