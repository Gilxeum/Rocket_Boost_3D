using UnityEngine;

[CreateAssetMenu(fileName = "New enemy", menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string enemyName;

    public GameObject enemyPrefab;

    [Header("Stats")]

    public int health;
    public int damage;
}
