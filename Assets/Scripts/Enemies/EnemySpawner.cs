using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Enemy prefab
    public GameObject powerfulEnemyPrefab;  // Powerful Enemy prefab
    public float spawnInterval = 4f;  // Seconds to wait between spawns
    public Transform[] spawnPoints;  // Spawn points
    public int powerfulEnemyFrequency = 5;  // Spawn a powerful enemy every 5 normal enemies

    public GameManager gameManager;

    private int enemySpawnCounter = 0;  // Track number of normal enemies spawned

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Check if the game is paused
            while (gameManager.isPaused)
            {
                yield return null;  // Wait for one frame and keep checking if the game is paused
            }

            // Randomize spawn point
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            // Spawning regular enemy
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemySpawnCounter++;

            // Spawn powerful enemy only every 'powerfulEnemyFrequency' spawns
            if (enemySpawnCounter >= powerfulEnemyFrequency)
            {
                Instantiate(powerfulEnemyPrefab, spawnPoint.position, Quaternion.identity);
                enemySpawnCounter = 0;  // Reset the counter after spawning a powerful enemy
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