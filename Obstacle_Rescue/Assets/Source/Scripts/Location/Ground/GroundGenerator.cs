using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class GroundGenerator : MonoBehaviour
{
    public Action<string> OnObstacleChunk {  get; private set; }

    private const int MaxChunks = 4;
    private const float PlayerBuffer = 40f;

    [SerializeField] private List<Ground> _groundPool;
    [SerializeField] private List<Ground> _obstacleGround;
    private readonly Dictionary<string, Ground> _obstacleDictionary = new();
    private readonly List<Ground> _activeChunks = new();

    private Ground _obstacleChunk;

    [Inject] private readonly Player _player;

    private void OnEnable() => OnObstacleChunk += ActivateObstacleChunk;
    private void OnDisable() => OnObstacleChunk -= ActivateObstacleChunk;
    private void Start()
    {
        _activeChunks.Add(_groundPool[0]);
        foreach (var platform in _obstacleGround)
        {
            if (platform is GroundTrap groundTrap)
                _obstacleDictionary.Add(groundTrap.name, platform);
        }
    }

    private void FixedUpdate()
    {
        if (_player == null) return;

        if (ShouldSpawnNextChunk())
            ActivateChunk();

        if (_activeChunks.Count > MaxChunks)
            RemoveOldChunk();
    }
    public void ClearJ() => _obstacleChunk = null;
    private bool ShouldSpawnNextChunk() =>
        _player.transform.position.x + PlayerBuffer > _activeChunks[^1].End.position.x;

    private void ActivateChunk()
    {
        if (_obstacleChunk != null) return;

        int index = UnityEngine.Random.Range(1, _groundPool.Count);
        Ground randomGround = _groundPool[index];
        randomGround.gameObject.SetActive(true);
        randomGround.transform.position = GetNextChunkPosition(randomGround);

        _groundPool.RemoveAt(index);
        _activeChunks.Add(randomGround);
    }
    private void ActivateObstacleChunk(string key)
    {
        if (_obstacleChunk != null) return;

        _obstacleChunk = _obstacleDictionary[key];
        _obstacleChunk.gameObject.SetActive(true);
        _obstacleChunk.transform.position = GetNextChunkPosition(_obstacleChunk);
        _obstacleChunk.GetComponent<GroundTrap>()._collider.enabled = true;

        _activeChunks.Add(_obstacleChunk);
        ActivateChunk();
    }
    private Vector2 GetNextChunkPosition(Ground nextGround) =>
        new(_activeChunks[^1].End.position.x + UnityEngine.Random.Range(0f, 0.4f) - nextGround.Begin.position.x,
            nextGround.transform.position.y + UnityEngine.Random.Range(-0.2f, 0.2f)
        );

    private void RemoveOldChunk()
    {
        Ground oldGround = _activeChunks[0];
        _activeChunks.RemoveAt(0);

        if (_groundPool.Count <= 1)
            _groundPool.Add(oldGround);

        oldGround.gameObject.SetActive(false);
    }
}