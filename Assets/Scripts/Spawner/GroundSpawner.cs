using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public static GroundSpawner Instance;

    [Tooltip("Prefab that contains Plane + Environment + EndTrigger")]
    public GameObject groundTilePrefab;

    [Tooltip("Player Transform (tagged 'Player')")]
    public Transform player;

    [Tooltip("Length of one tile in world Z units (use the longest child length).")]
    public float tileLength = 200f; // updated for your project

    float nextSpawnZ = 0f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Spawn initial tiles
        nextSpawnZ = 0f;
        for (int i = 0; i < 5; i++)
        {
            SpawnNextTile();
        }
    }

    public void SpawnNextTile()
    {
        if (groundTilePrefab == null)
        {
            Debug.LogError("GroundSpawner: groundTilePrefab is not assigned.");
            return;
        }

        Vector3 spawnPos = new Vector3(0f, 0f, nextSpawnZ);
        Instantiate(groundTilePrefab, spawnPos, Quaternion.identity);
        nextSpawnZ += tileLength;
    }
}
