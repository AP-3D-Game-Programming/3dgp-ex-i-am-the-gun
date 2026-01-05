using NUnit.Framework;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] levelEnemies;
    [SerializeField] GameObject levelBoss;
    private GameObject[] spawners;
    private GameObject bossSpawner;
    [SerializeField] float spawnInterval = 5;
    [SerializeField] float bossSpawnAfter = 300;
    private float timer = 0;
    private float bossTimer = 0;
    [SerializeField] int amountPerSpawn;
    private GameManager gameManager;
    private bool bossHasSpawned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
        bossSpawner = GameObject.Find("BossSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gameIsPaused)
        {
            timer += Time.deltaTime;
            bossTimer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                for (int i = 0; i < amountPerSpawn; i++)
                {
                    Instantiate(levelEnemies[Random.Range(0, levelEnemies.Length)], spawners[Random.Range(0, spawners.Length)].transform.position, Quaternion.identity);
                }
                timer = 0;
            }
            if ( bossTimer >= bossSpawnAfter && bossHasSpawned == false)
            {
                Instantiate(levelBoss, bossSpawner.transform.position, Quaternion.identity);
                bossHasSpawned = true;
            }
        }
    }
}
