using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        // Tell GameManager
        GameManager.Instance.OnPlayerHitObstacle();
    }
}
