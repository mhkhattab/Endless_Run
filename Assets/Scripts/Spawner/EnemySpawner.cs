using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;
    public float laneOffset = 2f;          // MUST match PlayerController laneDistance
    public List<EnemyData> enemyOptions;   // ScriptableObjects list

    private bool spawned = false;          // ensures only ONE enemy spawns

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;

        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if (spawned) return;  // prevent multiple spawns
        spawned = true;

        if (enemyOptions == null || enemyOptions.Count == 0)
        {
            Debug.LogWarning("EnemySpawner: No EnemyData assigned.");
            return;
        }

        // Pick a random enemy type
        EnemyData chosen = enemyOptions[Random.Range(0, enemyOptions.Count)];

        // Choose lane (or random if -1)
        int lane = chosen.lane < 0 ? Random.Range(0, 3) : chosen.lane;

        // Spawn position (20 units ahead of player)
        float spawnZ = player.position.z + 20f;
        float spawnY = 0.1f;
        float spawnX = (lane - 1) * laneOffset;

        Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);

        // Instantiate enemy prefab
        GameObject enemyObj = Instantiate(chosen.enemyPrefab, spawnPos, Quaternion.identity);

        // Initialize movement
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Init(chosen.speed, lane, laneOffset);
        }
    }
}
