using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int scoreValue = 10;
    public float bobAmplitude = 0.25f;
    public float bobSpeed = 2f;

    Vector3 start;

    void Start()
    {
        start = transform.position;
    }

    void Update()
    {
        transform.position = start + Vector3.up * Mathf.Sin(Time.time * bobSpeed) * bobAmplitude;
        transform.Rotate(Vector3.up * 60f * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.AddScore(scoreValue);
        // TODO: particle effect / sound
        Destroy(gameObject);
    }
}
