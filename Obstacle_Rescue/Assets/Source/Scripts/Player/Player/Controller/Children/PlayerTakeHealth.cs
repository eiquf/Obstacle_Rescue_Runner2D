using UnityEngine;

public class PlayerTakeHealth : PlayerSystem
{
    private MovementSettings _movementSettings;
    public PlayerTakeHealth
        (PlayerAnimation animation,
        MovementSettings movementSettings) : base(animation)
    {
        _movementSettings = movementSettings;
    }

    public override void Execute(Transform transform)
    {
        Vector2 healOrigin = new Vector2(_pos.x, _pos.y);
        RaycastHit2D healHitX = Physics2D.Raycast(healOrigin, Vector2.right, _velocity.x * Time.fixedDeltaTime, _movementSettings.HealLayerMask);
        if (healHitX.collider != null)
        {
            Heal heal = healHitX.collider.GetComponent<Heal>();
            if (heal != null)
            {
                heal.IsHealCollected?.Invoke();
                //here shpuld bew anim
            }
                
        }

        RaycastHit2D healHitY = Physics2D.Raycast(healOrigin, Vector2.up, _velocity.y * Time.fixedDeltaTime, _movementSettings.HealLayerMask);
        if (healHitY.collider != null)
        {
            Heal heal = healHitY.collider.GetComponent<Heal>();
            if (heal != null)
            {
                heal.IsHealCollected?.Invoke();
            }
        }
    }
}