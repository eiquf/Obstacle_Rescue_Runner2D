using System;
using UnityEngine;
using Zenject;

public sealed class Ground : MonoBehaviour
{
    public Action IsMoving;

    [SerializeField] private GroundSettings settings;
    private bool _fallPlatformAdded;
    private Transform _spawnPropTransform;

    private Transform _initialPos;
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
        _initialPos = transform;
        Begin = transform.GetChild(0);
        End = transform.GetChild(1);
        _collider = GetComponent<BoxCollider2D>();
    }
    private void CalculateHeight() => Height = transform.position.y + (_collider.bounds.extents.y);
    private void MoveGround()
    {
        transform.Translate(_player.Velocity.x * Time.fixedDeltaTime * Vector2.left);

        if (transform.position.x < settings.HidePositionX)
            gameObject.SetActive(false);
    }
    private void GenerateProps()
    {
        if (transform.Find("SpawnPoint")?.TryGetComponent(out _spawnPropTransform) == true)
            settings.PropsGenerator.Execute(_spawnPropTransform);
    }

    private void AddFallPlatformIfNeeded()
    {
        if (!_fallPlatformAdded && !CompareTag("Unfallable") && UnityEngine.Random.Range(0, 10) == settings.FallPlatformChance)
        {
            if (transform.position.x - 5 == _player.transform.position.x)
            {
                gameObject.AddComponent<GroundFall>().Initialize(_player);
                _fallPlatformAdded = true;
            }
        }
    }
    private void OnDisable() => transform.position = new(0, _initialPos.position.y, 0);
}