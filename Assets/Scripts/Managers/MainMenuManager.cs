using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // For scene management

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject mainMenu;
    
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
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        print("pressed");
    }
    
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
        print("pressed");
    }

    // Method to quit the game
    public void QuitGame()
    {
        Application.Quit();
        print("pressed");
    }
}
