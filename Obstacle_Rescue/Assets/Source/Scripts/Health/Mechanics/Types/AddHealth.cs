using UnityEngine;

public class AddHealth : Health
{
    public override void Execute()
    {
        if (hurts.Count < _livesSettings.MaxLives && hurts.Count != _livesSettings.MinLives)
        {
            GameObject hurtPref = Object.Instantiate(hurt, _healthFactory.transform);
            _propUIAnim.OnEnable(hurtPref.transform);
            hurts.Add(hurtPref);
        }
    }
}