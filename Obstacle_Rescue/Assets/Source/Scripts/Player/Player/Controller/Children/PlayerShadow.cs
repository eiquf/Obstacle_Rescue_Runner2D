using DG.Tweening;
using UnityEngine;

public class PlayerShadow : PlayerSystem
{
    public PlayerShadow(PlayerAnimation animation) : base(animation) { }
    public override void Execute(Transform transform)
    {
        transform.gameObject.SetActive(_isGrounded);

        transform.DOScale(_isGrounded ? new Vector3(5.3f, 1, 0) : new Vector3(0, 0), 0.12f)
           .SetEase(_isGrounded ? Ease.OutBounce : Ease.InBounce, 10f);
    }
}