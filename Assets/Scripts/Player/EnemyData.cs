using UnityEngine;

[CreateAssetMenu(menuName = "RunBeyond/EnemyData")]
public class EnemyData : ScriptableObject
{
    public GameObject enemyPrefab;
    public float speed = 5f;
    public int damage = 1;
    public float spawnChance = 1f;
    public int lane = -1; // -1 = random lane, 0/1/2 for specific
}
