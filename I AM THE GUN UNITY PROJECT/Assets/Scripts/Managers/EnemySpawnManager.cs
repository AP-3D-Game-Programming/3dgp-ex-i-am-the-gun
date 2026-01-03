using NUnit.Framework;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] levelEnemies;
    private GameObject[] spawners;
    [SerializeField] float spawnInterval = 5;
    private float timer = 0;
    [SerializeField] int amountPerSpawn;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gameIsPaused)
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                for (int i = 0; i < amountPerSpawn; i++)
                {
                    Instantiate(levelEnemies[Random.Range(0, levelEnemies.Length)], spawners[Random.Range(0, spawners.Length)].transform);
                }
                timer = 0;
            }
        }
    }
}
