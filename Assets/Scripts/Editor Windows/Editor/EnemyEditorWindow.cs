using UnityEditor;
using UnityEngine;

public class EnemyEditorWindow : EditorWindow
{
    private string enemyName = "New Enemy";
    private GameObject enemyPrefab;
    private float health = 100f;
    private float speed = 5f;

    // Add the window to the Unity Editor under "Window" -> "Enemy Editor"
    [MenuItem("Window/Enemy Editor")]
    public static void ShowWindow()
    {
        GetWindow<EnemyEditorWindow>("Enemy Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create New Enemy", EditorStyles.boldLabel);

        // Input fields for enemy properties
        enemyName = EditorGUILayout.TextField("Enemy Name", enemyName);
        enemyPrefab = (GameObject)EditorGUILayout.ObjectField("Enemy Prefab", enemyPrefab, typeof(GameObject), false);
        health = EditorGUILayout.FloatField("Health", health);
        speed = EditorGUILayout.FloatField("Speed", speed);

        // Button to create the new enemy data
        if (GUILayout.Button("Create New Enemy"))
        {
            CreateNewEnemy();
        }
    }

    // Method to create and save the new enemy
    private void CreateNewEnemy()
    {
        // Create a new EnemyData ScriptableObject instance
        EnemyData newEnemy = CreateInstance<EnemyData>();

        // Assign values from the editor fields
        newEnemy.enemyName = enemyName;
        newEnemy.enemyPrefab = enemyPrefab;
        newEnemy.health = health;
        newEnemy.speed = speed;

        // Save the new enemy asset in the "Assets/Enemies" folder
        string assetPath = $"Assets/Enemies/{enemyName}.asset";
        AssetDatabase.CreateAsset(newEnemy, assetPath);
        AssetDatabase.SaveAssets();

        // Confirmation message
        EditorUtility.DisplayDialog("Enemy Created", $"Enemy '{enemyName}' has been created and saved at {assetPath}.", "OK");
    }
}
