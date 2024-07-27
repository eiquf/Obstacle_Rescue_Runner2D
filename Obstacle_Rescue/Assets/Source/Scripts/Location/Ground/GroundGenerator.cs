using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class GroundGenerator : MonoBehaviour
{
    public Action<int> IsWordComplete;

    private const int MaxSpawnedChunks = 7;
    private const float PlayerPosXAllowence = 40f;

    private int _currentIndex;

    private bool _wordIsComplete = false;
    public bool WordIsComplete
    {
        get => _wordIsComplete;
        set => _wordIsComplete = value;
    }

    private Ground _firstChunk;

    private readonly List<Ground> spawnedChunks = new();
    private GameObject[] _platforms;

    [Inject] private readonly Player _player;
    private void OnEnable() => _platforms = new GroundPlatformsSetter().Execute();
    private void Awake()
    {
        _firstChunk = transform.GetChild(0).GetComponent<Ground>();
        spawnedChunks.Add(_firstChunk);

        _currentIndex = 0;
    }
    private void FixedUpdate()
    {
        if (_platforms == null || _player == null) return;

        if (_player.transform.position.x + PlayerPosXAllowence > spawnedChunks[^1].End.position.x)
            SpawnPlatform();

        RemoveOldChunks();
    }
    private void SpawnPlatform()
    {
        float randomAddY = UnityEngine.Random.Range(-0.5f, 0.5f);
        Ground newChunk = Instantiate(_platforms[_currentIndex], transform).GetComponent<Ground>();
        newChunk.transform.position = new Vector2(spawnedChunks[^1].End.position.x - newChunk.Begin.position.x, newChunk.Begin.position.x + randomAddY);
        spawnedChunks.Add(newChunk);

        _currentIndex++;

        InjectComponentsInChunks();
    }
    private void InjectComponentsInChunks()
    {
        foreach (Ground ground in spawnedChunks)
            ground.Inject(_player);
    }
    private void RemoveOldChunks()
    {
        if (spawnedChunks.Count <= MaxSpawnedChunks) return;

        Destroy(spawnedChunks[0].gameObject);
        spawnedChunks.RemoveAt(0);

        if (_currentIndex <= 5) return;

        _currentIndex = 0;
    }
}