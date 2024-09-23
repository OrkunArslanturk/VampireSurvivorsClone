using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Enemy prefab
    public float spawnInterval = 2f;  // Seconds to wait spawn
    public Transform[] spawnPoints;  // Spawn points

    public GameManager gameManager;

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

            // Spawning enemy
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

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
