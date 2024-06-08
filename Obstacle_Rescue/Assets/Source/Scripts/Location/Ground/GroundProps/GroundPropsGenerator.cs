using UnityEngine;

public class GroundPropsGenerator : MonoBehaviour
{
    private const int _indexHealsSpawn = 7, _indexObstaclesSpawn = 4;
    private const string SpawnPointAPI = "SpawnPoint";

    private Transform _spawnPointTransform;

    private GroundPropsSetter _groundPropsSetter;
    public void Inject(GroundPropsSetter groundPropsSetter) => _groundPropsSetter = groundPropsSetter;
    private void OnEnable()
    {
        _spawnPointTransform = transform.Find(SpawnPointAPI).GetComponent<Transform>();
        PropToInstance();
    }
    private void PropToInstance()
    {
        int random = Random.Range(0, 10);
        switch (random)
        {
            case _indexHealsSpawn: HealsInstantiate(); break;
            case _indexObstaclesSpawn: ObstacleInstantiate(); break;
        }
    }
    private void HealsInstantiate()
    {
        int randomIndexHeals = Random.Range(0, _groundPropsSetter.heals.Length);
        Heal heal = Instantiate(_groundPropsSetter.heals[randomIndexHeals], _spawnPointTransform);
    }
    private void ObstacleInstantiate()
    {
        int randomIndexObstacles = Random.Range(0, _groundPropsSetter.obstacles.Length);
        Instantiate(_groundPropsSetter.obstacles[randomIndexObstacles], _spawnPointTransform);
    }
}