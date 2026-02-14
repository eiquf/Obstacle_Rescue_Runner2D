using UnityEngine;

[CreateAssetMenu(fileName = "Movement settings", menuName = "Movement settings")]
public class MovementSettings : ScriptableObject
{
    #region Fields

    [field: Header("Move parametres")]
    [field: SerializeField] public float gravity { get; set; } = -150f;
    [field: SerializeField][field: Range(10, 50)] public float MaxXVelocity { get; set; } = 50f;
    [field: SerializeField][field: Range(0, 10)] public float MaxAcceleration { get; set; } = 10f;

    [field: Header("Jump parametres")]
    [field: SerializeField] public float JumpVelocity { get; set; } = 20f;
    [field: SerializeField] public float MaxMaxHoldJumpTime { get; set; } = 0.4f;
    [field: SerializeField] public float JumpGroundThreshold { get; set; } = 1f;
    [field: Header("Else")]
    [field: SerializeField] public float DeadLine { get; set; } = -25f;
    [field: SerializeField] public float StopVelocity { get; set; } = 0.7f;
    [field: SerializeField] public float PosXallowance { get; set; } = 0.7f;
    [field: Header("Layer Masks")]
    [field: SerializeField] public LayerMask GroundLayerMask { get; private set; }
    [field: SerializeField] public LayerMask ObstacleLayerMask { get; private set; }
    [field: SerializeField] public LayerMask TrapLayerMask { get; private set; }
    [field: SerializeField] public LayerMask HealLayerMask { get; private set; }
    [field: SerializeField] public LayerMask LetterLayerMask { get; private set; }
    #endregion

    #region Constant variables
    private const float _maxAccelerationConst = 10;
    private const float _posXallowanceConst = 0.7f;
    private const float _deadLineConst = -25f;
    #endregion

    private void OnValidate()
    {
        if (MaxAcceleration != _maxAccelerationConst) MaxAcceleration = _maxAccelerationConst;
        if (PosXallowance != _posXallowanceConst) PosXallowance = _posXallowanceConst;
        if (DeadLine != _deadLineConst) DeadLine = _deadLineConst;
    }
}