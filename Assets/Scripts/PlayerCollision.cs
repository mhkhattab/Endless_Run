using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            GameManager.Instance.PlayerDied();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            Collectible c = other.GetComponent<Collectible>();
            if (c != null) c.Collect();
        }
    }
}
