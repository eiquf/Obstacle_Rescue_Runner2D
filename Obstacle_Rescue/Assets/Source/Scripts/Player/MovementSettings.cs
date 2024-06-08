using UnityEngine;

[CreateAssetMenu(fileName = "Movement settings", menuName = "Movement settings")]
public class MovementSettings : ScriptableObject
{
    #region Fields

    [field: Header("Move parametres")]
    [field: SerializeField] public float gravity { get; set; } = -150f;
    [field: SerializeField][field: Range(10, 50)] public float maxXVelocity { get; set; } = 50f;
    [field: SerializeField][field: Range(0, 10)] public float maxAcceleration { get; set; } = 10f;

    [field: Header("Jump parametres")]
    [field: SerializeField] public float jumpVelocity { get; set; } = 20f;
    [field: SerializeField] public float maxMaxHoldJumpTime { get; set; } = 0.4f;
    [field: SerializeField] public float jumpGroundThreshold { get; set; } = 1f;
    [field: Header("Else")]
    [field: SerializeField] public float deadLine { get; set; } = -25f;
    [field: SerializeField] public float stopVelocity { get; set; } = 0.7f;
    [field: SerializeField] public float posXallowance { get; set; } = 0.7f;
    [field: Header("Layer Masks")]
    [field: SerializeField] public LayerMask groundLayerMask { get; private set; }
    [field: SerializeField] public LayerMask obstacleLayerMask { get; private set; }
    [field: SerializeField] public LayerMask healLayerMask { get; private set; }
    [field: SerializeField] public LayerMask letterLayerMask { get; private set; }
    [field: Header("Bounce")]
    [field: SerializeField] public float minX = -5f;
    [field: SerializeField] public float maxX = 5f;
    [field: SerializeField] public float minY = -5f;
    [field: SerializeField] public float maxY = 5f;
    [field: SerializeField] public float bounceForce = 5f;
    #endregion

    #region Constant variables
    private const float _maxXValocityConst = 50;
    private const float _maxAccelerationConst = 10;
    private const float _stopVelocityConst = 0.7f;
    private const float _posXallowanceConst = 0.7f;
    private const float _deadLineConst = -25f;
    #endregion

    private void OnValidate()
    {
        if (maxXVelocity != _maxXValocityConst) maxXVelocity = _maxXValocityConst;
        if (maxAcceleration != _maxAccelerationConst) maxAcceleration = _maxAccelerationConst;
        if (stopVelocity != _stopVelocityConst) stopVelocity = _stopVelocityConst;
        if (posXallowance != _posXallowanceConst) posXallowance = _posXallowanceConst;
        if (deadLine != _deadLineConst) deadLine = _deadLineConst;
    }
}