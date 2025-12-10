using UnityEngine;
using System.Collections.Generic;

public class CollectibleSpawner : MonoBehaviour
{
    public Transform spawnOrigin;
    public float spawnZDistance = 30f;
    public float laneOffset = 2f;
    public List<CollectibleData> collectibleOptions;
    public float spawnInterval = 2.5f;
    float timer = 0f;

    void Update()
    {
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
        CollectibleData chosen = collectibleOptions[Random.Range(0, collectibleOptions.Count)];
        int lane = Random.Range(0, 3);
        Vector3 pos = spawnOrigin.position + spawnOrigin.forward * spawnZDistance + spawnOrigin.right * ((lane - 1) * laneOffset);
        GameObject go = Instantiate(chosen.collectiblePrefab, pos, Quaternion.identity);
        var c = go.GetComponent<Collectible>();
        if (c != null) c.scoreValue = chosen.scoreValue;
    }
}
