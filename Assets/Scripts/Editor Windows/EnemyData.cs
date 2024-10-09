using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Create New Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyName;  // Name of the enemy
    public GameObject enemyPrefab;  // Prefab for the enemy
    public float health;  // Health of the enemy
    public float speed;  // Speed of the enemy
    public int damage;  // Damage dealt by the enemy
    public int scoreValue;  // How much score is awarded for killing the enemy
}
