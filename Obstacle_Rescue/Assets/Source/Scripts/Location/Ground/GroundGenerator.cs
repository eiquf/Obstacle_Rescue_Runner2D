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

    [SerializeField] private Ground _firstChunk;

    private readonly List<Ground> spawnedChunks = new();
    private GameObject[] _platforms;

    [Inject] private readonly Player _player;
    private void Awake()
    {
        _platforms = new GroundPlatformsSetter().Execute();

        _firstChunk = transform.GetChild(0).GetComponent<Ground>();
        spawnedChunks.Add(_firstChunk);

        _currentIndex = 0;
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
        if (_player != null && _player.transform.position.x + PlayerPosXAllowence > spawnedChunks[^1].End.position.x)
            Chunks();

        InjectComponentsInChunks();
    }
    private void InjectComponentsInChunks()
    {
        foreach (Ground ground in spawnedChunks)
            ground.Inject(_player);
    }
    private void Chunks()
    {
        float randomAddY = UnityEngine.Random.Range(-1.5f, 1.5f);
        Ground newChunk = Instantiate(_platforms[_currentIndex], transform).GetComponent<Ground>();
        newChunk.transform.position = new Vector2(spawnedChunks[^1].End.position.x - newChunk.Begin.position.x, transform.position.y + randomAddY);
        spawnedChunks.Add(newChunk);

        _currentIndex++;
    }
    private void RemoveChuncks()
    {
        if (spawnedChunks.Count == MaxSpawnedChunks)
            spawnedChunks.RemoveAt(0);
    }
}