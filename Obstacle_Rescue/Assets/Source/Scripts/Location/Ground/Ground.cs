using System;
using UnityEngine;
using Zenject;

public sealed class Ground : MonoBehaviour
{
    public Action IsMoving;

    private const int FallPlatformChance = 4;
    private const float HidePositionX = -200f;

    private bool _fallPlatformAdded;
    private Transform _spawnPropTransform;

    [field: SerializeField] public bool isChangableY { get; private set; }
    public Transform Begin { get; private set; }
    public Transform End { get; private set; }
    public float Height { get; private set; }

    private BoxCollider2D _collider;

    [Inject] private Player _player;

    private void Awake()
    {
        CacheComponents();
        CalculateHeight();
        GenerateProps();
    }

    private void FixedUpdate()
    {
        if (_player == null) return;

        AddFallPlatformIfNeeded();
        MoveGround();
    }

    private void CacheComponents()
    {
        Begin = transform.GetChild(0);
        End = transform.GetChild(1);
        _collider = GetComponent<BoxCollider2D>();
    }
    private void CalculateHeight() => Height = transform.position.y + (_collider.size.y * 0.5f);
    private void MoveGround()
    {
        transform.Translate(_player.Velocity.x * Time.fixedDeltaTime * Vector2.left);

        if (transform.position.x < HidePositionX)
            gameObject.SetActive(false);
    }
    private void GenerateProps()
    {
        if (transform.Find("SpawnPoint")?.TryGetComponent(out _spawnPropTransform) == true)
            new GroundPropsGenerator().Execute(_spawnPropTransform);
    }

    private void AddFallPlatformIfNeeded()
    {
        if (!_fallPlatformAdded && !CompareTag("Unfallable") && UnityEngine.Random.Range(0, 10) == FallPlatformChance)
        {
            if (transform.position.x - 5 == _player.transform.position.x)
            {
                gameObject.AddComponent<GroundFall>().Initialize(_player);
                _fallPlatformAdded = true;
            }
        }
    }
}