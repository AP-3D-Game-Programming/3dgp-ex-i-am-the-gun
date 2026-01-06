using UnityEngine;
using UnityEngine.AI;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private GameObject[] levelEnemies;
    [SerializeField] private GameObject levelBoss;

    [Header("Spawners")]
    private GameObject[] spawners;
    private GameObject bossSpawner;

    [Header("Spawn Timing")]
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float bossSpawnAfter = 300f;
    [SerializeField] private int amountPerSpawn = 3;

    private float timer = 0f;
    private float bossTimer = 0f;
    private bool bossHasSpawned = false;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        spawners = GameObject.FindGameObjectsWithTag("Spawner");
        bossSpawner = GameObject.Find("BossSpawn");
    }

    void Update()
    {
        if (gameManager.gameIsPaused)
            return;

        timer += Time.deltaTime;
        bossTimer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemies();
            timer = 0f;
        }

        if (bossTimer >= bossSpawnAfter && !bossHasSpawned)
        {
            SpawnBoss();
            bossHasSpawned = true;
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < amountPerSpawn; i++)
        {
            GameObject enemyPrefab = levelEnemies[Random.Range(0, levelEnemies.Length)];
            GameObject spawner = spawners[Random.Range(0, spawners.Length)];

            SpawnOnNavMesh(enemyPrefab, spawner.transform.position);
        }
    }

    private void SpawnBoss()
    {
        SpawnOnNavMesh(levelBoss, bossSpawner.transform.position);
    }

    private void SpawnOnNavMesh(GameObject prefab, Vector3 spawnPosition)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPosition, out hit, 10f, NavMesh.AllAreas))
        {
            GameObject enemy = Instantiate(prefab, hit.position, Quaternion.identity);

            // Extra safety: force agent to snap correctly
            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.Warp(hit.position);
            }
        }
        else
        {
            Debug.LogWarning("Failed to find NavMesh near spawn point: " + spawnPosition);
        }
    }
}
