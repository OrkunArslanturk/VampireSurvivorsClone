using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject mainMenu;

    public TextMeshProUGUI killCountText;  // Reference to the TextMeshPro element

    void Start()
    {
        settingsMenu.SetActive(false);

        // Check if KillCounter.Instance is available
        if (KillCounter.Instance != null)
        {
            int totalKills = KillCounter.Instance.GetTotalKills();
            killCountText.text = "Total Kills: " + totalKills.ToString();
        }
        else
        {
            Debug.LogError("KillCounter instance is null. Ensure KillCounter is in the scene.");
            killCountText.text = "Total Kills: 0";  // Default display in case instance is null
        }
    }

    public void ResetKills()
    {
        if (KillCounter.Instance != null)
        {
            KillCounter.Instance.ResetTotalKills();
            Debug.Log("Total kills have been reset.");
        }
        else
        {
            Debug.LogError("KillCounter instance is null. Ensure KillCounter is in the scene.");
        }
    }

    // Method to start the game
    public void StartGame()
    {
        // Load the game scene (replace "GameScene" with your actual gameplay scene name)
        SceneManager.LoadScene("GameScene");
    }

    // Method to open settings (could be a separate settings panel)
    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    // Method to quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
