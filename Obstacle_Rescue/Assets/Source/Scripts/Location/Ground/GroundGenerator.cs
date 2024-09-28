using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class GroundGenerator : MonoBehaviour
{
    public event Action<int> OnWordComplete;

    private const int MaxChunks = 7;
    private const float PlayerBuffer = 40f;

    private bool _wordIsComplete;
    public bool WordIsComplete
    {
        get => _wordIsComplete;
        set => _wordIsComplete = value;
    }

    [SerializeField] private List<Ground> _groundPool = new();
    private readonly List<Ground> _activeChunks = new();

    [Inject] private Player _player;

    private void Awake() => InitializePlatforms();

    private void FixedUpdate()
    {
        if (_player == null) return;

        if (ShouldSpawnNextChunk())
            ActivateChunk();

        RemoveOldChunk();
    }

    private void InitializePlatforms()
    {
        _groundPool.AddRange(GetComponentsInChildren<Ground>());
        _activeChunks.Add(_groundPool[0]);

        for (int i = 1; i < _groundPool.Count; i++)
            _groundPool[i].gameObject.SetActive(false);
    }

    private bool ShouldSpawnNextChunk() =>
        _player.transform.position.x + PlayerBuffer > _activeChunks[^1].End.position.x;

    private void ActivateChunk()
    {
        var randomGround = _groundPool[UnityEngine.Random.Range(1, _groundPool.Count)];
        randomGround.transform.position = GetNextChunkPosition(randomGround);
        randomGround.gameObject.SetActive(true);
        _activeChunks.Add(randomGround);
    }

    private Vector2 GetNextChunkPosition(Ground nextGround)
    {
        float randomOffset = UnityEngine.Random.Range(-0.5f, 0.5f);
        return new Vector2(
            _activeChunks[^1].End.position.x - nextGround.Begin.position.x + randomOffset,
            nextGround.Begin.position.y + randomOffset);
    }

    private void RemoveOldChunk()
    {
        if (_activeChunks.Count <= MaxChunks) return;

        var oldGround = _activeChunks[0];
        _activeChunks.RemoveAt(0);
        _groundPool.Add(oldGround);
    }
}