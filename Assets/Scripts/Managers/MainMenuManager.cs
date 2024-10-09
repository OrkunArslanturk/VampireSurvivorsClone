using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject mainMenu;

    public TextMeshProUGUI killCountText;

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

    public void StartGame()
    {
        // Load the game scene
        SceneManager.LoadScene("GameScene");
    }

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

    public void QuitGame()
    {
        Application.Quit();
    }
}
