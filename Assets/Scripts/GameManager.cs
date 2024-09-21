using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public GameObject settingsMenu;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure game starts unpaused
        isPaused = false;

        // Show the main menu if needed
        if (mainMenu != null)
        {
            ShowMainMenu();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Listen for the pause input (Escape key)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true); // Show pause menu

        // Disable all objects that shouldn't be active during pause (e.g., player, enemies, etc.)
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

    void DisableGameObjects()
    {
        // Example: Disable player input and movement
        Player player = FindObjectOfType<Player>();  // Assuming your player script is named Player
        if (player != null)
        {
            player.enabled = false;
        }

        // Example: Disable all enemies
        Enemy[] enemies = FindObjectsOfType<Enemy>();  // Assuming your enemy script is named Enemy
        foreach (Enemy enemy in enemies)
        {
            enemy.enabled = false;
        }
    }

    // Method to enable gameplay objects after resuming
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
    }

    // Method to load the main menu (if needed)
    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
    }

    // Method to hide the main menu and start the game
    public void StartGame()
    {
        mainMenu.SetActive(false);
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
        settingsMenu.SetActive(true);
    }

    // Method to close settings menu
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }

}
