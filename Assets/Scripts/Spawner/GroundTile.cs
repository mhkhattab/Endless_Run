using UnityEngine;

public class GroundTile : MonoBehaviour
{
    public float tileLength = 20f;  // YOU WILL CHANGE THIS TO YOUR REAL VALUE

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GroundSpawner.Instance.SpawnNextTile();
            Destroy(gameObject, 2f);
        }
    }
}
