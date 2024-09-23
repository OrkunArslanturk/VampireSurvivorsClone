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

    [Header("Barrel Settings")]
    public Transform barrel;
    private Vector3 originalBarrelPosition; 
    public float recoilDistance = 0.033f;  // Distance for the recoil

    void Start()
    {
        // Store the original barrel position for resetting after each fire
        originalBarrelPosition = barrel.localPosition;
    }

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
        // Recoil effect, move barrel backward along the Y axis
        Vector3 recoilPosition = new Vector3(barrel.localPosition.x, originalBarrelPosition.y + recoilDistance, barrel.localPosition.z);
        barrel.localPosition = recoilPosition;

        // Instantiate the bullet at firePoint position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody2D component and apply velocity to move the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = -firePoint.up * bulletSpeed;  // Keep the bullet moving in its original direction

        // Reset the barrel position after firing
        Invoke("ResetBarrelPosition", 0.1f);  // Wait a short time before resetting
    }

    void ResetBarrelPosition()
    {
        barrel.localPosition = originalBarrelPosition;
    }
}
