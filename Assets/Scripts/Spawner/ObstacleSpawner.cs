using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public Transform spawnOrigin; // position in front of player where spawns start
    public float spawnZDistance = 40f;
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
        // accelerate difficulty slightly
        if (difficulty != null)
        {
            spawnInterval = Mathf.Max(difficulty.minSpawnInterval, spawnInterval - difficulty.spawnAcceleration * Time.deltaTime);
            // slowly increment player forward speed
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

        // weighted pick
        float total = 0f;
        foreach (var o in obstacleOptions) total += o.spawnChance;
        float r = Random.Range(0f, total);
        float acc = 0f;
        ObstacleData chosen = obstacleOptions[0];
        foreach (var o in obstacleOptions)
        {
            acc += o.spawnChance;
            if (r <= acc) { chosen = o; break; }
        }

        int lane = chosen.lane;
        if (lane < 0) lane = Random.Range(0, 3); // 0,1,2

        Vector3 pos = spawnOrigin.position + spawnOrigin.forward * spawnZDistance + spawnOrigin.right * ((lane - 1) * laneOffset);
        GameObject go = Instantiate(chosen.obstaclePrefab, pos, Quaternion.identity);
        float scale = Random.Range(chosen.minScale, chosen.maxScale);
        go.transform.localScale = Vector3.one * scale;
    }
}
