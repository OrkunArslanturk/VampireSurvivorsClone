using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event System.Action OnEnemyKilled;

    [Header("Health")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int health;

    [Header("Movement")]
    public float moveSpeed = 2f;
    protected Transform player;

    [Header("XP Orb Settings")]
    public GameObject xpOrbPrefab;  //XP orb prefab
    public float xpSpawnChance = 0.8f;  // 80% chance to spawn XP orb

    protected virtual void Start()
    {
        health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        MoveTowardsPlayer();
        RotateTowardsPlayer();
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        // XP Orb spawn
        float randomValue = Random.Range(0f, 1f);
        if (randomValue <= xpSpawnChance)
        {
            Instantiate(xpOrbPrefab, transform.position, Quaternion.identity);
        }

        OnEnemyKilled?.Invoke();

        Destroy(gameObject);
    }

    protected virtual void MoveTowardsPlayer()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    protected virtual void RotateTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(10);
        }
    }
}
