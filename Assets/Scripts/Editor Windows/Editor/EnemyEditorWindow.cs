using UnityEditor;
using UnityEngine;

public class EnemyEditorWindow : EditorWindow
{
    private string enemyName = "New Enemy";
    private GameObject enemyPrefab;
    private float health = 100f;
    private float speed = 5f;

    [MenuItem("Window/Enemy Editor")]
    public static void ShowWindow()
    {
        GetWindow<EnemyEditorWindow>("Enemy Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create New Enemy", EditorStyles.boldLabel);

        enemyName = EditorGUILayout.TextField("Enemy Name", enemyName);
        enemyPrefab = (GameObject)EditorGUILayout.ObjectField("Enemy Prefab", enemyPrefab, typeof(GameObject), false);
        health = EditorGUILayout.FloatField("Health", health);
        speed = EditorGUILayout.FloatField("Speed", speed);

        if (GUILayout.Button("Create New Enemy"))
        {
            CreateNewEnemy();
        }
    }

    private void CreateNewEnemy()
    {
        EnemyData newEnemy = CreateInstance<EnemyData>();

        newEnemy.enemyName = enemyName;
        newEnemy.enemyPrefab = enemyPrefab;
        newEnemy.health = health;
        newEnemy.speed = speed;

        string assetPath = $"Assets/Enemies/{enemyName}.asset";
        AssetDatabase.CreateAsset(newEnemy, assetPath);
        AssetDatabase.SaveAssets();

        EditorUtility.DisplayDialog("Enemy Created", $"Enemy '{enemyName}' has been created and saved at {assetPath}.", "OK");
    }
}
