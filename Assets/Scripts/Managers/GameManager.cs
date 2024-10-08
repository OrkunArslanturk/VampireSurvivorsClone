using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    public bool isPaused = false;
    private bool isInSettings = false;

    void Start()
    {
        isPaused = false;
    }

    void Update()
    {
        // pause input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && !isInSettings)
            {
                ResumeGame();
            }
            else if (!isPaused && !isInSettings)
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true); // Show pause menu

        DisableGameObjects();
    }

    // Method to resume the game
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);  // Hide pause menu

        // Enable the objects again when resuming
        EnableGameObjects();
    }
    
    public void MainMenu()
    {
        // Ensure the current kill count is saved before quitting
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    void DisableGameObjects()
    {
        Player player = FindObjectOfType<Player>(); 
        if (player != null)
        {
            player.enabled = false;
        }

        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.enabled = false;
        }

        MorePowerfulEnemy[] morePowerfulEnemies = FindObjectsOfType<MorePowerfulEnemy>();
        foreach (MorePowerfulEnemy morePowerfulEnemy in morePowerfulEnemies)
        {
            morePowerfulEnemy.enabled = false;
        }
    }

    // enable gameplay objects after resuming
    void EnableGameObjects()
    {
        // Enable player
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.enabled = true;
        }

        // Enable all enemies
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.enabled = true;
        }

        MorePowerfulEnemy[] morePowerfulEnemies = FindObjectsOfType<MorePowerfulEnemy>();
        foreach (MorePowerfulEnemy morePowerfulEnemy in morePowerfulEnemies)
        {
            morePowerfulEnemy.enabled = true;
        }
    }

    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();  // This will quit the game in a build
    }

    // Method to open settings menu
    public void OpenSettings()
    {
        isInSettings = true;
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);  // Hide pause menu
    }

    // Method to close settings menu
    public void CloseSettings()
    {
        isInSettings = false;
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true); // Show pause menu
    }

}
