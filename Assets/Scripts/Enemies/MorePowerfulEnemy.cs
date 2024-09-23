using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorePowerfulEnemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] int maxHealth;
    [SerializeField] int health;

    [Header("Movement")]
    public float moveSpeed = 1f;
    private Transform player;

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
            // Yön vektörü hesaplama (Player'ın pozisyonuna göre)
            Vector2 direction = (player.position - transform.position).normalized;

            // Açıyı hesapla
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotasyonu uygula
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
