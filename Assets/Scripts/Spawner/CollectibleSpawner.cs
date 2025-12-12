using UnityEngine;
using System.Collections.Generic;

public class CollectibleSpawner : MonoBehaviour
{
    public Transform spawnOrigin;
    public float laneOffset = 2f;
    public List<CollectibleData> collectibleOptions;
    public float spawnInterval = 2.5f;

    float timer = 0f;
    Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnRandom();
            timer = 0f;
        }
    }

    void SpawnRandom()
    {
        if (collectibleOptions == null || collectibleOptions.Count == 0) return;

        // Pick random collectible
        CollectibleData chosen = collectibleOptions[Random.Range(0, collectibleOptions.Count)];

        // Lane 0, 1, or 2
        int lane = Random.Range(0, 3);

        // Random Z distance ahead of player
        float randomDistance = Random.Range(20f, 80f);

        float targetZ = player.position.z + randomDistance;

        // Final position
        Vector3 pos = new Vector3(
            (lane - 1) * laneOffset,
            spawnOrigin.position.y + 0.5f,  // slightly above ground
            targetZ
        );

        // Create collectible
        Instantiate(chosen.collectiblePrefab, pos, Quaternion.identity);
    }
}
