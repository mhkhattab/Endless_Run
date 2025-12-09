using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "Runner/ObstacleData")]
public class ObstacleData : ScriptableObject
{
    public GameObject obstaclePrefab;
    public float spawnChance = 1f;
    public float minScale = 1f;
    public float maxScale = 1f;
}
