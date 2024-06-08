using DG.Tweening;
using System;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public Action IsHealCollected;

    private float _levitationHeight = 1f;
    private HealthFactory _health;
    private void OnEnable() => IsHealCollected += DestroyHeal;
    private void OnDisable()
    {
        IsHealCollected -= DestroyHeal;
        _health.OnPlayerHealed?.Invoke();
    }
    private void Awake() => _health = FindFirstObjectByType<HealthFactory>();
    private void Start() => Animation();
    private void DestroyHeal() => Destroy(gameObject);
    private void Animation()
    {
        float targetY = transform.position.y + _levitationHeight;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(targetY, _levitationHeight / 2f).SetEase(Ease.Linear));
        sequence.Append(transform.DOMoveY(transform.position.y, _levitationHeight / 2f).SetEase(Ease.Linear));
        sequence.SetLoops(-1);
    }
}