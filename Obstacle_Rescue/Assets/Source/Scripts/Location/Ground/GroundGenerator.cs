using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public sealed class GroundGenerator : MonoBehaviour
{
    public Action<int> IsWordComplete;
    private enum TrapPlatforms
    {
        BoatPlatform,
        BridgePlatform,
        BucketPlatform
    }
    private const int IndexGroundPropGenerator = 7;
    private const int MaxSpawnedChunks = 7;
    private const float PlayerPosXAllowence = 40f;

    private int _currentIndex;

    private bool _wordIsComplete = false;
    public bool WordIsComplete
    {
        get => _wordIsComplete;
        set => _wordIsComplete = value;
    }

    [SerializeField] private Ground _firstChunk;

    private readonly List<Ground> spawnedChunks = new();
    private GameObject[] _platforms;

    private Player _player;
    private Health _health;
    private GroundPropsSetter _groundPropsSetter;
    private GroundPlatformsSetter _platformsSetter;

    [Inject]
    private void Construct(
        Player player,
        Health health,
        GroundPropsSetter groundPropsSetter,
        GroundPlatformsSetter groundPlatformsSetter)
    {
        _player = player;
        _health = health;
        _groundPropsSetter = groundPropsSetter;
        _platformsSetter = groundPlatformsSetter;
    }
    private void Awake()
    {
        _platforms = new GroundPlatformsSetter().Execute();

        _firstChunk = transform.GetChild(0).GetComponent<Ground>();
        spawnedChunks.Add(_firstChunk);
    }
    private void FixedUpdate()
    {
        SpawnPlatform();
        ClearCurrentIndex();
        RemoveChuncks();
    }
    private void ClearCurrentIndex()
    {
        if (_currentIndex >= 5)
            _currentIndex = 0;
    }
    private void SpawnPlatform()
    {
        if (_player != null && _player.transform.position.x + PlayerPosXAllowence > spawnedChunks[spawnedChunks.Count - 1].End.position.x)
            DefaultChunks();
        else return;

        InjectComponentsInChunks();
    }
    private void InjectComponentsInChunks()
    {
        foreach (Ground ground in spawnedChunks)
            ground.Inject(_player);
    }
    private void DefaultChunks()
    {
        float randomAddY = UnityEngine.Random.Range(-1.5f, 1.5f);
        Ground newChunk = Instantiate(_platforms[_currentIndex], transform).GetComponent<Ground>();
        newChunk.transform.position = new Vector2(spawnedChunks[^1].End.position.x - newChunk.Begin.position.x, transform.position.y + randomAddY);
        spawnedChunks.Add(newChunk);

        PlatformWithProp(newChunk);

        _currentIndex++;
    }
    private void PlatformWithProp(Ground ground)
    {
        int randomGeneratorIndex = UnityEngine.Random.Range(0, 10);

        if (randomGeneratorIndex == IndexGroundPropGenerator)
            ground.AddComponent<GroundPropsGenerator>().Inject(_groundPropsSetter);
    }
    private void RemoveChuncks()
    {
        if (spawnedChunks.Count == MaxSpawnedChunks)
            spawnedChunks.RemoveAt(0);
    }
}