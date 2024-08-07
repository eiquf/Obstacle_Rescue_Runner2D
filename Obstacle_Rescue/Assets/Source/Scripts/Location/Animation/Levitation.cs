using DG.Tweening;
using UnityEngine;

public class Levitation
{
    private const float LevitationHeight = 1f;
    public void Execute(Transform transform) => Animation(transform);
    private void Animation(Transform transform)
    {
        float targetY = transform.position.y + LevitationHeight;
        transform.DOMoveY(targetY, LevitationHeight / 2f)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Yoyo);
    }
}