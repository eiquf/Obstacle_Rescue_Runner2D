using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GroundPropsSetter : MonoBehaviour
{
    public Action<int> OnGrounPropChosen;

    #region Heals
    private string[] _prefabsHealsAPI = new string[]
    {
        "FluffyHurt",
        "GoldApple"
    };
    private List<Heal> healsList = new List<Heal>();
    public Heal[] heals { get; private set; }
    #endregion
    #region Obstacles
    private string[] _prefabsObstaclesAPI = new string[]
    {
        //"Stone"
    };
    private List<Obstacle> obstaclesList = new List<Obstacle>();
    public Obstacle[] obstacles { get; private set; }
    #endregion
    private void Start()
    {
        LoadHeals();
        LoadObstacles();
    }
    private void LoadHeals()
    {
        for (int i = 0; i < _prefabsHealsAPI.Length; i++)
        {
            Addressables.LoadAssetAsync<GameObject>(_prefabsHealsAPI[i]).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject loadedHeal = handle.Result;
                    Heal healComponent = loadedHeal.GetComponent<Heal>();

                    if (healComponent != null)
                    {
                        healsList.Add(healComponent);
                        if (healsList.Count == _prefabsHealsAPI.Length)
                            heals = healsList.ToArray();
                    }
                    Addressables.Release(handle);
                }
                else Debug.LogError("Something went wrong with heals prefabs load!");
            };
        }
    }
    private void LoadObstacles()
    {
        for (int i = 0; i < _prefabsObstaclesAPI.Length; i++)
        {
            Addressables.LoadAssetAsync<GameObject>(_prefabsObstaclesAPI[i]).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject loadedObstacle = handle.Result;
                    Obstacle obstacleComponent = loadedObstacle.GetComponent<Obstacle>();

                    if (obstacleComponent != null)
                    {
                        obstaclesList.Add(obstacleComponent);
                        if (obstaclesList.Count == _prefabsObstaclesAPI.Length)
                            obstacles = obstaclesList.ToArray();
                    }
                    Addressables.Release(handle);
                }
                else Debug.LogError("Something went wrong with obstacles prefabs load!");
            };
        }
    }
}