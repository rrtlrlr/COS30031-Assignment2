using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int maxZombies = 10;
    [SerializeField] private float spawnInterval = 3f;

    private float _nextSpawnTime;

    private void Update()
    {
        if (Time.time >= _nextSpawnTime && FindObjectsByType<ZombieMovement>(FindObjectsSortMode.None).Length < maxZombies)
        {
            SpawnZombie();
            _nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnZombie()
    {
        if (spawnPoints.Length == 0 || zombiePrefab == null)
        {
            return;
        }

        int index = Random.Range(0, spawnPoints.Length);
        Instantiate(zombiePrefab, spawnPoints[index].position, Quaternion.identity);
    }
}