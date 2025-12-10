using UnityEngine;

public class PlayerMovementSimple : MonoBehaviour
{
    public float forwardSpeed = 5f;          // always move forward
    public float laneDistance = 2.5f;        // distance between lanes
    public float laneChangeSpeed = 10f;      // how fast we slide to lane

    private int targetLane = 1; // 0 = left, 1 = middle, 2 = right

    void Update()
    {
        // --- ALWAYS MOVE FORWARD LIKE RUNNER ---
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // --- LANE INPUT ---
        if (Input.GetKeyDown(KeyCode.A))
            targetLane = Mathf.Max(0, targetLane - 1);

        if (Input.GetKeyDown(KeyCode.D))
            targetLane = Mathf.Min(2, targetLane + 1);

        // --- CALCULATE TARGET POSITION ON X ---
        float targetX = (targetLane - 1) * laneDistance;
        Vector3 newPos = transform.position;
        newPos.x = Mathf.Lerp(transform.position.x, targetX, laneChangeSpeed * Time.deltaTime);
        transform.position = newPos;
    }
}
