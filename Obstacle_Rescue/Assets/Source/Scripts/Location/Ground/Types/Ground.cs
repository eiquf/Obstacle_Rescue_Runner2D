using UnityEngine;

public abstract class Ground : MonoBehaviour
{
    [SerializeField] protected GroundSettings Settings;
    [SerializeField] protected Transform _spawnPropTransform;

    protected bool _fallPlatformAdded;

    protected Transform _initialPos;
    public Transform Begin { get; protected set; }
    public Transform End { get; protected set; }
    public float Height { get; protected set; }

    protected BoxCollider2D Collider;

    protected abstract void Initialize();
    protected abstract void Move();
    protected abstract void GenerateProps();
}