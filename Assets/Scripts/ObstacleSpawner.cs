using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public List<ObstacleData> obstacleTypes;
    public float spawnInterval = 1.2f;
    public Transform spawnPoint; // position reference (x/y used for spawn base)
    public float spawnZOffset = 30f; // ahead of player
    public float laneDistance = 2f;
    public int lanes = 3;
    public float spawnRandomVariance = 0.4f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnObstacle();
            float wait = spawnInterval + Random.Range(-spawnRandomVariance, spawnRandomVariance);
            wait = Mathf.Max(0.2f, wait);
            yield return new WaitForSeconds(wait);
        }
    }

    void SpawnObstacle()
    {
        if (player == null || obstacleTypes.Count == 0) return;

        float total = 0f;
        foreach (var o in obstacleTypes) total += o.spawnChance;
        float r = Random.Range(0f, total);
        ObstacleData chosen = obstacleTypes[0];
        float accum = 0f;
        foreach (var o in obstacleTypes)
        {
            accum += o.spawnChance;
            if (r <= accum) { chosen = o; break; }
        }

        int lane = Random.Range(0, lanes);
        Vector3 basePos = spawnPoint != null ? spawnPoint.position : player.position;
        Vector3 spawnPos = new Vector3((lane - 1) * laneDistance, basePos.y, player.position.z + spawnZOffset);
        GameObject go = Instantiate(chosen.obstaclePrefab, spawnPos, Quaternion.identity);
        float s = Random.Range(chosen.minScale, chosen.maxScale);
        go.transform.localScale *= s;
        go.tag = "Obstacle";
        go.transform.SetParent(transform);
    }
}
