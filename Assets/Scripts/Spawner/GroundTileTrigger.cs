using UnityEngine;

public class GroundTileTrigger : MonoBehaviour
{
    [Tooltip("Delay before destroying the parent tile (seconds)")]
    public float destroyDelay = 2f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // spawn next tile
        if (GroundSpawner.Instance != null)
            GroundSpawner.Instance.SpawnNextTile();

        // prevent double-trigger
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        // destroy the whole tile (root of this trigger) after a short delay
        Destroy(transform.root.gameObject, destroyDelay);
    }
}
