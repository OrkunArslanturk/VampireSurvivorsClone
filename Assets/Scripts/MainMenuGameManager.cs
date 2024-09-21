using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management

public class MainMenuGameManager : MonoBehaviour
{
    public GameObject settingsMenu;
    
    void Start()
    {
        settingsMenu.SetActive(false);
    }
    // Method to start the game
    public void StartGame()
    {
        // Load the game scene (replace "GameScene" with your actual gameplay scene name)
        SceneManager.LoadScene("GameScene");
        print("pressed");
    }

    // Method to open settings (could be a separate settings panel)
    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
        print("pressed");
    }

    // Method to quit the game
    public void QuitGame()
    {
        Application.Quit();
        print("pressed");
    }
}
