using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Health playerHealth;
    [SerializeField] private GameObject zombiePrefab;

    [Header("Spawning")]
    [SerializeField] private int maximumAliveZombies = 8;
    [SerializeField] private float minimumSpawnInterval = 5f;
    [SerializeField] private float maximumSpawnInterval = 15f;
    [SerializeField] private float minimumSpawnDistance = 12f;
    [SerializeField] private float maximumSpawnDistance = 25f;
    [SerializeField] private int spawnAttempts = 15;
    [SerializeField] private float navMeshSearchRadius = 2f;

    [Header("Horde Settings")]
    [SerializeField] private int maximumHordeSize = 5;
    [SerializeField, Range(0.01f, 1f)] private float hordeDecayFactor = 0.5f;
    [SerializeField] private float hordeRadius = 2.5f;

    [Header("Spacing")]
    [SerializeField] private float minimumZombieDistance = 3f;

    private readonly List<GameObject> spawnedZombies = new List<GameObject>();
    private Coroutine spawningCoroutine;

    private void OnEnable()
    {
        spawningCoroutine = StartCoroutine(SpawningLoop());
    }

    private void OnDisable()
    {
        if (spawningCoroutine != null)
            StopCoroutine(spawningCoroutine);
    }

    private IEnumerator SpawningLoop()
    {
        while (true)
        {
            float delay = Random.Range(minimumSpawnInterval, maximumSpawnInterval);

            yield return new WaitForSeconds(delay);

            TrySpawnZombie();
        }
    }

    private void TrySpawnZombie()
    {
        if (player == null || playerCamera == null ||
            playerHealth == null || zombiePrefab == null)
            return;

        if (playerHealth.Current <= 0)
            return;

        CleanupZombieList();

        int remainingCapacity = maximumAliveZombies - CountAliveZombies();

        if (remainingCapacity <= 0)
            return;

        int hordeSize = Mathf.Min(ChooseHordeSize(), remainingCapacity);

        for (int attempt = 0; attempt < spawnAttempts; attempt++)
        {
            if (TryFindHordePositions(hordeSize, out List<Vector3> positions))
            {
                SpawnHorde(positions);
                return;
            }
        }
    }

    private int ChooseHordeSize()
    {
        float totalWeight = 0f;

        for (int i = 0; i < maximumHordeSize; i++)
            totalWeight += Mathf.Pow(hordeDecayFactor, i);

        float randomValue = Random.Range(0f, totalWeight);

        for (int i = 0; i < maximumHordeSize; i++)
        {
            float weight = Mathf.Pow(hordeDecayFactor, i);

            if (randomValue < weight)
                return i + 1;

            randomValue -= weight;
        }

        return 1;
    }

    private bool TryFindHordePositions(int hordeSize, out List<Vector3> positions)
    {
        positions = new List<Vector3>();

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float distance = Random.Range(minimumSpawnDistance, maximumSpawnDistance);

        Vector3 centerPosition = player.position + new Vector3(
            randomDirection.x * distance,
            0f,
            randomDirection.y * distance
        );

        if (!NavMesh.SamplePosition(
            centerPosition,
            out NavMeshHit centerHit,
            navMeshSearchRadius,
            NavMesh.AllAreas))
            return false;

        centerPosition = centerHit.position;

        for (int i = 0; i < hordeSize; i++)
        {
            Vector2 offset = hordeSize == 1
                ? Vector2.zero
                : Random.insideUnitCircle * hordeRadius;

            Vector3 candidatePosition = centerPosition + new Vector3(
                offset.x,
                0f,
                offset.y
            );

            if (!NavMesh.SamplePosition(
                candidatePosition,
                out NavMeshHit hit,
                navMeshSearchRadius,
                NavMesh.AllAreas))
                return false;

            Vector3 spawnPosition = hit.position;

            float playerDistance = Vector3.Distance(spawnPosition, player.position);

            if (playerDistance < minimumSpawnDistance ||
                playerDistance > maximumSpawnDistance)
                return false;

            if (IsVisibleToPlayer(spawnPosition) ||
                IsTooCloseToAnotherZombie(spawnPosition))
                return false;

            foreach (Vector3 existingPosition in positions)
            {
                if (Vector3.Distance(spawnPosition, existingPosition) < minimumZombieDistance)
                    return false;
            }

            positions.Add(spawnPosition);
        }

        return positions.Count == hordeSize;
    }

    private void SpawnHorde(List<Vector3> positions)
    {
        foreach (Vector3 spawnPosition in positions)
        {
            Vector3 direction = player.position - spawnPosition;
            direction.y = 0f;

            Quaternion rotation = direction.sqrMagnitude > 0.01f
                ? Quaternion.LookRotation(direction)
                : Quaternion.identity;

            GameObject zombie = Instantiate(zombiePrefab, spawnPosition, rotation);

            ZombieAI zombieAI = zombie.GetComponent<ZombieAI>();

            if (zombieAI != null)
                zombieAI.Initialize(player, playerHealth);

            spawnedZombies.Add(zombie);
        }
    }

    private bool IsVisibleToPlayer(Vector3 position)
    {
        Vector3 viewportPosition = playerCamera.WorldToViewportPoint(
            position + Vector3.up
        );

        return viewportPosition.z > 0f &&
               viewportPosition.x >= 0f &&
               viewportPosition.x <= 1f &&
               viewportPosition.y >= 0f &&
               viewportPosition.y <= 1f;
    }

    private bool IsTooCloseToAnotherZombie(Vector3 position)
    {
        foreach (GameObject zombie in spawnedZombies)
        {
            if (zombie == null)
                continue;

            Health health = zombie.GetComponent<Health>();

            if (health == null || health.Current <= 0)
                continue;

            if (Vector3.Distance(position, zombie.transform.position) < minimumZombieDistance)
                return true;
        }

        return false;
    }

    private int CountAliveZombies()
    {
        int count = 0;

        foreach (GameObject zombie in spawnedZombies)
        {
            if (zombie == null)
                continue;

            Health health = zombie.GetComponent<Health>();

            if (health != null && health.Current > 0)
                count++;
        }

        return count;
    }

    private void CleanupZombieList()
    {
        spawnedZombies.RemoveAll(zombie => zombie == null);
    }
}