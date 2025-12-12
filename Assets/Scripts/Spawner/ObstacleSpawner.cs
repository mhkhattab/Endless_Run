using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform spawnOrigin; // base position for spawning
    public float laneOffset = 2f;
    public List<ObstacleData> obstacleOptions;
    public DifficultySettings difficulty;

    float spawnTimer = 0f;
    float spawnInterval;

    Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
        spawnInterval = difficulty != null ? difficulty.initialSpawnInterval : 1.5f;
    }

    void Update()
    {
        if (player == null) return;

        spawnTimer += Time.deltaTime;

        // Difficulty scaling
        if (difficulty != null)
        {
            spawnInterval = Mathf.Max(difficulty.minSpawnInterval,
                spawnInterval - difficulty.spawnAcceleration * Time.deltaTime);

            // Increase player forward speed over time
            var pc = player.GetComponent<PlayerController>();
            if (pc) pc.forwardSpeed += difficulty.forwardSpeedIncreasePerSecond * Time.deltaTime;
        }

        if (spawnTimer >= spawnInterval)
        {
            SpawnRandomObstacle();
            spawnTimer = 0f;
        }
    }

    void SpawnRandomObstacle()
    {
        if (obstacleOptions == null || obstacleOptions.Count == 0) return;

        // ---- 1) Weighted random pick ----
        float total = 0f;
        foreach (var o in obstacleOptions) total += o.spawnChance;

        float r = Random.Range(0f, total);
        float acc = 0f;
        ObstacleData chosen = obstacleOptions[0];

        foreach (var o in obstacleOptions)
        {
            acc += o.spawnChance;
            if (r <= acc)
            {
                chosen = o;
                break;
            }
        }

        // ---- 2) Pick lane ----
        int lane = chosen.lane < 0 ? Random.Range(0, 3) : chosen.lane;

        // ---- 3) Pick random Z distance ahead of player ----
        float randomDistanceAhead = Random.Range(30f, 100f);

        float targetZ = player.position.z + randomDistanceAhead;

        // ---- 4) Spawn position ----
        Vector3 pos = new Vector3(
            (lane - 1) * laneOffset,
            spawnOrigin.position.y,
            targetZ
        );

        // ---- 5) Instantiate ----
        GameObject go = Instantiate(chosen.obstaclePrefab, pos, Quaternion.identity);

        float scale = Random.Range(chosen.minScale, chosen.maxScale);
        go.transform.localScale = Vector3.one * scale;
    }
}
