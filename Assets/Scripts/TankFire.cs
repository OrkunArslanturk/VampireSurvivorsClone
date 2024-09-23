using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFire : MonoBehaviour
{
    [Header("Fire Settings")]
    public GameObject bulletPrefab;  // Bullet prefab
    public Transform firePoint;  // Bullet spawn point
    public float bulletSpeed = 10f;  // Bullet speed
    public float fireInterval = 0.5f;  // Time interval between shots

    private bool isFiring = false;  // To check if firing is active

    public GameManager gameManager;
    
    void Update()
    {
        // Start firing if not already firing
        if (!isFiring)
        {
            StartCoroutine(FireRoutine());
        }
    }

    // Coroutine that handles the continuous firing
    IEnumerator FireRoutine()
    {
        isFiring = true;

        while (true)  // Keep firing as long as the game is running
        {
            // Check if the game is paused
            while (gameManager.isPaused)
            {
                yield return null;  // Wait for one frame and recheck until the game is unpaused
            }
            Fire();  // Shoot a bullet

            // Wait for the specified interval between shots, but break early if the game is paused
            float elapsedTime = 0f;
            while (elapsedTime < fireInterval)
            {
                if (gameManager.isPaused)
                {
                    yield return null;  // Wait until unpaused
                }
                else
                {
                    elapsedTime += Time.deltaTime;
                    yield return null;  // Continue timing until the fire interval is reached
                }
            }
        }
    }

    void Fire()
    {
        // Instantiate the bullet at firePoint position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody2D component and apply velocity to move the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = -firePoint.up * bulletSpeed;
    }
}
