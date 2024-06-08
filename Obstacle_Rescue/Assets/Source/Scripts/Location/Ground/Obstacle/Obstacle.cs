using System;
using UnityEngine;

public sealed class Obstacle : MonoBehaviour
{
    public Action IsDestroyed;
    private void OnEnable() => IsDestroyed += ObstacleCollisioned;
    private void OnDisable() => IsDestroyed -= ObstacleCollisioned;
    private void ObstacleCollisioned() => Destroy(gameObject);
}