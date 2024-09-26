using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] int maxHealth;
    [SerializeField] int health;

    [Header("Movement")]
    public float moveSpeed = 2f;
    private Transform player;

    [Header("XP Orb Settings")]
    public GameObject xpOrbPrefab;  // Reference to the XP orb prefab
    public float xpSpawnChance = 0.8f;  // 80% chance to spawn XP orb

    void Start()
    {
        health = maxHealth;

        // Finding player in scene
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        MoveTowardsPlayer();
        RotateTowardsPlayer();
    }

    void TakeDamage(int someDamage)
    {
        health -= someDamage;
        if (health <= 0) Death();
    }

    void Death()
    {
        // Check if we should spawn an XP orb (80% chance)
        float randomValue = Random.Range(0f, 1f);
        if (randomValue <= xpSpawnChance)
        {
            Instantiate(xpOrbPrefab, transform.position, Quaternion.identity);
        }

        // Destroy the enemy game object
        Destroy(gameObject);
    }

    void MoveTowardsPlayer()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void RotateTowardsPlayer()
    {
        if (player != null)
        {
            // calculate the vector
            Vector2 direction = (player.position - transform.position).normalized;

            // calculate angle
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // apply rotation
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10);
        }
    }
}
