using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class GroundGenerator : MonoBehaviour
{
    public event Action OnObstacleChunk;

    private const int MaxChunks = 7;
    private const float PlayerBuffer = 40f;

    [SerializeField] private List<Ground> _groundPool;
    [SerializeField] private List<Ground> _obstacleGroundPool;
    private List<Ground> _activeChunks = new();

    private Ground _obstacleChunk;

    [Inject] private readonly Player _player;

    private void OnEnable() => OnObstacleChunk += ActivateObstacleChunk;
    private void OnDisable() => OnObstacleChunk -= ActivateObstacleChunk;
    private void Start() => _activeChunks.Add(_groundPool[0]);

    private void FixedUpdate()
    {
        if (_player == null) return;

        if (ShouldSpawnNextChunk())
            ActivateChunk();

        if (_activeChunks.Count > MaxChunks)
            RemoveOldChunk();
    }

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
    private void ActivateObstacleChunk()
    {
        int index = UnityEngine.Random.Range(1, _obstacleGroundPool.Count);
        _obstacleChunk = _obstacleGroundPool[index];
        _obstacleChunk.gameObject.SetActive(true);
        _obstacleChunk.transform.position = GetNextChunkPosition(_obstacleChunk);

        _activeChunks.Add(_obstacleChunk);
    }
    private Vector2 GetNextChunkPosition(Ground nextGround) =>
        new(_activeChunks[^1].End.position.x + UnityEngine.Random.Range(0f, 0.4f) - nextGround.Begin.position.x,
            nextGround.transform.position.y + UnityEngine.Random.Range(-0.2f, 0.2f)
        );

    private void RemoveOldChunk()
    {
        Ground oldGround = _activeChunks[0];
        _activeChunks.RemoveAt(0);

        if (_obstacleChunk == null)
            _groundPool.Add(oldGround);

        oldGround.gameObject.SetActive(false);
    }
}