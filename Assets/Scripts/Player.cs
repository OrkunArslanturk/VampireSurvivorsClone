using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] int maxHealth;
    [SerializeField] int health;

    [Header("Damage Settings")]
    [SerializeField] int damagePerSecond = 10;  // Damage per second
    [SerializeField] float damageInterval = 2f;  // Damage interval
    private float damageTimer;  // Damage timer

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        damageTimer = damageInterval; // Damage timer start
    }

    void Update()
    {
        Inputs();
        Move();

        if (!enabled) return;  // If the script is disabled, stop the player from receiving input
    }

    void FixedUpdate()
    {
        Move();
    }

    // WASD
    void Inputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");  // A and D
        float moveY = Input.GetAxisRaw("Vertical");    // W and S
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = moveDirection * moveSpeed;

        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;  // Angle calculation
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));  // rotation
        }

    }

    void TakeDamage(int someDamage)
    {
        health -= someDamage;
        if (health <= 0) Death();
    }

    void Death()
    {
        print("Player Died");

        Time.timeScale = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (damageTimer <= 0f)
            {
                TakeDamage(damagePerSecond);
                damageTimer = damageInterval;
            }
            else
            {
                damageTimer -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            damageTimer = damageInterval;
        }
    }
}
