using UnityEngine;

public class KillCounter : MonoBehaviour
{
    public static KillCounter Instance;
    public int enemyKillCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        Enemy.OnEnemyKilled += IncrementKillCount;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= IncrementKillCount;
    }

    private void IncrementKillCount()
    {
        enemyKillCount++;
        SaveKillCount();
    }

    private void SaveKillCount()
    {
        int totalKills = PlayerPrefs.GetInt("TotalKills", 0);
        totalKills += 1;
        PlayerPrefs.SetInt("TotalKills", totalKills);
        PlayerPrefs.Save();
    }

    public int GetTotalKills()
    {
        return PlayerPrefs.GetInt("TotalKills", 0);
    }

    // Reset the total kills
    public void ResetTotalKills()
    {
        PlayerPrefs.SetInt("TotalKills", 0);
        PlayerPrefs.Save();
    }
}
