using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Import for TextMeshPro
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("XP")]
    [SerializeField] int currentExperience;
    [SerializeField] int maxExperience;
    [SerializeField] int currentLevel;
    int xpAmount = 25;  // Amount of XP gained per orb

    [Header("UI Elements")]
    public TextMeshProUGUI levelText;  // Use TextMeshProUGUI for UI Text
    public TextMeshProUGUI HPText;
    public Slider experienceBar;  // Slider for experience bar
    public GameObject UpgradeCanvas;  // The upgrade canvas for level up options
    public Button increaseHPButton;  // Button to increase HP
    public Button shootFasterButton;  // Button to decrease fire interval
    public GameObject GameoverCanvas;

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

    [Header("Tank Fire")]
    public TankFire tankFire;  // Reference to the TankFire script (separate script)

    private void OnEnable()
    {
        ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
    }

    private void OnDisable()
    {
        ExperienceManager.Instance.OnExperienceChange -= HandleExperienceChange;
    }

    void Start()
    {
        UpgradeCanvas.SetActive(false);
        GameoverCanvas.SetActive(false);
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        damageTimer = damageInterval; // Damage timer start

        // Initialize the UI
        UpdateLevelUI();
        UpdateExperienceBar();
        UpdateHPUI();  // Update HP Text at the start

        // Assign button actions for upgrades
        increaseHPButton.onClick.AddListener(IncreaseHP);
        shootFasterButton.onClick.AddListener(ShootFaster);
    }

    void Update()
    {
        Inputs();
        Move();

        if (!enabled) return;  // If the script is disabled, stop the player from receiving input

        if (currentLevel == 4)
        {
            EnemySpawner.spawnInterval = 3f;
        }

        if (currentLevel == 8)
        {
            EnemySpawner.spawnInterval = 2f;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    // WASD Input Handling
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

    // Handle Experience Change
    private void HandleExperienceChange(int newExperience)
    {
        currentExperience += newExperience;
        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }

        UpdateExperienceBar();  // Update experience bar whenever experience changes
    }

    // Handle Level Up
    private void LevelUp()
    {
        UpgradeCanvas.SetActive(true);
        Time.timeScale = 0f;  // Pause game to let the player select an upgrade

        health = maxHealth;
        currentLevel++;
        currentExperience = 0;  // Reset current experience
        maxExperience += 20;  // Increase max experience needed for the next level

        // Update the UI
        UpdateLevelUI();
        UpdateHPUI();
        UpdateExperienceBar();
    }

    // Increase the player's HP by 50
    public void IncreaseHP()
    {
        maxHealth += 50;
        UpdateHPUI();
        CloseUpgradeCanvas();
    }

    // Decrease TankFire's fireInterval by 0.1 seconds
    public void ShootFaster()
    {
        if (tankFire != null)
        {
            tankFire.fireInterval -= 0.1f;  // Decrease fire interval
        }
        CloseUpgradeCanvas();
    }

    // Close the upgrade canvas and resume the game
    private void CloseUpgradeCanvas()
    {
        UpgradeCanvas.SetActive(false);
        Time.timeScale = 1f;  // Resume game
    }

    // Update Level Text UI
    private void UpdateLevelUI()
    {
        levelText.text = "Level: " + currentLevel;
    }

    private void UpdateHPUI()
    {
        HPText.text = "HP: " + health;
    }

    // Update Experience Bar Slider UI
    private void UpdateExperienceBar()
    {
        experienceBar.value = (float)currentExperience / maxExperience;
    }

    // Handle player damage
    void TakeDamage(int someDamage)
    {
        health -= someDamage;
        if (health <= 0) Death();

        UpdateHPUI();  // Update HP UI when the player takes damage
    }

    // OnTriggerEnter for XP Orbs and Enemy Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);  // Take damage on contact with enemy
        }

        if (collision.gameObject.CompareTag("XPOrb"))
        {
            Destroy(collision.gameObject);  // Destroy the XP orb
            print("XP Orb Collected");
            ExperienceManager.Instance.AddExperience(xpAmount);  // Add experience
        }
    }

    // Continuous damage from enemies
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

    // Reset the damage timer when exiting enemy contact
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            damageTimer = damageInterval;
        }
    }

    // Handle Player Death
    void Death()
    {
        print("Player Died");
        Time.timeScale = 0f;
        GameoverCanvas.SetActive(true);
    }
}
