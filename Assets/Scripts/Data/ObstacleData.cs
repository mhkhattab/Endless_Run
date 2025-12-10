using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "RunBeyond/ObstacleData")]
public class ObstacleData : ScriptableObject
{
    public GameObject obstaclePrefab;
    public float spawnChance = 1f; // relative weight
    public float minScale = 1f;
    public float maxScale = 1f;
    public int lane = -1; // -1 => random lane
}
