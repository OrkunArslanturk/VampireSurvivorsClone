using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Regular enemy prefab
    public GameObject powerfulEnemyPrefab;  // Powerful enemy prefab
    public GameObject mostPowerfulEnemyPrefab;  // Most powerful enemy prefab
    public static float spawnInterval = 4f;  // Seconds to wait between spawns
    public Transform[] spawnPoints;  // Spawn points
    public int powerfulEnemyFrequency = 5;  // Spawn a powerful enemy every 5 regular enemies
    public int mostPowerfulEnemyFrequency = 15;  // Spawn a most powerful enemy every 15 regular enemies

    public GameManager gameManager;

    private int enemySpawnCounter = 0;  // Track number of regular enemies spawned
    private int powerfulEnemyCounter = 0;  // Track number of powerful enemies spawned

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            while (gameManager.isPaused)
            {
                yield return null;
            }

            // Randomize spawn point
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            // Spawn regular enemy
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemySpawnCounter++;
            powerfulEnemyCounter++;

            // Spawn powerful enemy
            if (enemySpawnCounter >= powerfulEnemyFrequency)
            {
                Instantiate(powerfulEnemyPrefab, spawnPoint.position, Quaternion.identity);
                enemySpawnCounter = 0;  // Reset the counter after spawning a powerful enemy
            }

            // Spawn most powerful enemy
            if (powerfulEnemyCounter >= mostPowerfulEnemyFrequency)
            {
                Instantiate(mostPowerfulEnemyPrefab, spawnPoint.position, Quaternion.identity);
                powerfulEnemyCounter = 0;  // Reset the counter after spawning the most powerful enemy
            }

            // Wait for the spawn interval while also checking if the game is paused
            float elapsedTime = 0f;
            while (elapsedTime < spawnInterval)
            {
                if (gameManager.isPaused)
                {
                    yield return null;  // Pause waiting if the game is paused
                }
                else
                {
                    elapsedTime += Time.deltaTime;  // Continue timing if the game is not paused
                    yield return null;  // Yield to the next frame
                }
            }
        }
    }
}
