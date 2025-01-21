using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class GroundGenerator : MonoBehaviour
{
    private const int MaxChunks = 7;
    private const float PlayerBuffer = 40f;

    [SerializeField] private List<Ground> _groundPool;
    private List<Ground> _activeChunks = new();

    [Inject] private readonly Player _player;

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
        int index = Random.Range(1, _groundPool.Count);
        Ground randomGround = _groundPool[index];
        randomGround.gameObject.SetActive(true);
        randomGround.transform.position = GetNextChunkPosition(randomGround);

        _groundPool.RemoveAt(index);
        _activeChunks.Add(randomGround);
    }

    private Vector2 GetNextChunkPosition(Ground nextGround) =>
        new(_activeChunks[^1].End.position.x + Random.Range(0f, 0.4f) - nextGround.Begin.position.x,
            nextGround.transform.position.y + Random.Range(-0.2f, 0.2f)
        );

    private void RemoveOldChunk()
    {
        Ground oldGround = _activeChunks[0];
        _activeChunks.RemoveAt(0);
        _groundPool.Add(oldGround);
        oldGround.gameObject.SetActive(false);
    }
}