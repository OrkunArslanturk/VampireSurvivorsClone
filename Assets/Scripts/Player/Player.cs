using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("XP")]
    [SerializeField] int currentExperience;
    [SerializeField] int maxExperience;
    [SerializeField] int currentLevel;
    int xpAmount = 25;

    [Header("UI Elements")]
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI HPText;
    public Slider experienceBar;
    public GameObject UpgradeCanvas;
    public Button increaseHPButton;
    public Button shootFasterButton;
    public GameObject GameoverCanvas;

    [Header("Health")]
    [SerializeField] int maxHealth;
    [SerializeField] int health;

    [Header("Damage Settings")]
    [SerializeField] int damagePerSecond = 10;
    [SerializeField] float damageInterval = 2f;
    private float damageTimer;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    [Header("Tank Fire")]
    public TankFire tankFire;

    public UpgradeData healthUpgrade;
    public UpgradeData speedUpgrade;
    public UpgradeData damageUpgrade;

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
        damageTimer = damageInterval;

        UpdateLevelUI();
        UpdateExperienceBar();
        UpdateHPUI();

        increaseHPButton.onClick.AddListener(() => ApplyUpgrade(healthUpgrade));
        shootFasterButton.onClick.AddListener(() => ApplyUpgrade(speedUpgrade));
    }

    void Update()
    {
        Inputs();
        Move();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Inputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = moveDirection * moveSpeed;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));
        }
    }

    private void HandleExperienceChange(int newExperience)
    {
        currentExperience += newExperience;
        if (currentExperience >= maxExperience)
        {
            LevelUp();
        }

        UpdateExperienceBar();
    }

    private void LevelUp()
    {
        UpgradeCanvas.SetActive(true);
        Time.timeScale = 0f;

        health = maxHealth;
        currentLevel++;
        currentExperience = 0;
        maxExperience += 20;

        UpdateLevelUI();
        UpdateHPUI();
        UpdateExperienceBar();
    }

    public void ApplyUpgrade(UpgradeData upgrade)
    {
        switch (upgrade.upgradeType)
        {
            case UpgradeData.UpgradeType.Health:
                maxHealth += (int)upgrade.upgradeValue;
                break;
            case UpgradeData.UpgradeType.Speed:
                moveSpeed += upgrade.upgradeValue;
                break;
            case UpgradeData.UpgradeType.Damage:
                damageInterval = Mathf.Max(0.1f, damageInterval - upgrade.upgradeValue);
                break;
        }

        Debug.Log($"Applied {upgrade.upgradeName}: {upgrade.upgradeType} +{upgrade.upgradeValue}");
        CloseUpgradeCanvas();
    }


    private void CloseUpgradeCanvas()
    {
        UpgradeCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    private void UpdateLevelUI()
    {
        levelText.text = "Level: " + currentLevel;
    }

    private void UpdateHPUI()
    {
        HPText.text = "HP: " + health;
    }

    private void UpdateExperienceBar()
    {
        experienceBar.value = (float)currentExperience / maxExperience;
    }

    void TakeDamage(int someDamage)
    {
        health -= someDamage;
        if (health <= 0) Death();
        UpdateHPUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }

        if (collision.gameObject.CompareTag("XPOrb"))
        {
            Destroy(collision.gameObject);
            ExperienceManager.Instance.AddExperience(xpAmount);
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

    void Death()
    {
        Time.timeScale = 0f;
        GameoverCanvas.SetActive(true);
    }
}
