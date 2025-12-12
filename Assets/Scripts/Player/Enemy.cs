using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Enemy : MonoBehaviour
{
    private Transform player;
    private float speed = 5f;
    private int lane = 1;
    private float laneOffset = 2f;

    public void Init(float enemySpeed, int enemyLane, float laneOffsetValue)
    {
        speed = enemySpeed;
        lane = enemyLane;
        laneOffset = laneOffsetValue;

        // Snap to lane
        Vector3 pos = transform.position;
        pos.x = (lane - 1) * laneOffset;
        transform.position = pos;

        // Snap to ground
        SnapToGround();
    }

    void SnapToGround()
    {
        RaycastHit hit;

        // Cast a ray downward from above the enemy
        if (Physics.Raycast(transform.position + Vector3.up * 5f, Vector3.down, out hit, 20f))
        {
            Vector3 pos = transform.position;
            pos.y = hit.point.y;
            transform.position = pos;
        }
        else
        {
            Debug.LogWarning("Enemy could not find ground with raycast!");
        }
    }

    void Start()
    {
        var p = GameObject.FindWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        transform.position += direction.normalized * speed * Time.deltaTime;

        // Rotate toward player
        Vector3 lookAtPos = player.position;
        lookAtPos.y = transform.position.y;
        transform.LookAt(lookAtPos);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Enemy hit the player!");

        // Trigger your game over logic
        if (GameManager.Instance != null)
            GameManager.Instance.OnPlayerHitObstacle();
    }
}
