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
            Fire();  // Shoot a bullet
            yield return new WaitForSeconds(fireInterval);  // Wait for the specified time
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
