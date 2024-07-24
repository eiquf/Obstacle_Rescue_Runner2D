using DG.Tweening;
using System;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public Action IsHealCollected;

    private const float LevitationHeight = 1f;
    private HealthFactory _health;

    private void OnEnable() => IsHealCollected += DestroyHeal;
    private void OnDisable()
    {
        IsHealCollected -= DestroyHeal;
        _health.OnPlayerHealed?.Invoke();
    }

    private void Awake() => _health = FindFirstObjectByType<HealthFactory>();
    private void Start() => Animate();
    private void DestroyHeal() => Destroy(gameObject);
    private void Animate()
    {
        float targetY = transform.position.y + LevitationHeight;
        transform.DOMoveY(targetY, LevitationHeight / 2f)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Yoyo);
    }
}
