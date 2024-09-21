using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // Enemy prefab
    public float spawnInterval = 2f;  // Seconds to wait spawn
    public Transform[] spawnPoints;  // Spawn points

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Randomize spawn point
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            // Spawning enemy
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            // Wait to spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
