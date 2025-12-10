using UnityEngine;

public class PlayerMovementSimple : MonoBehaviour
{
    public float forwardSpeed = 5f;       // Constant forward speed
    public float laneDistance = 2.5f;     // Distance between lanes
    public float laneChangeSpeed = 10f;   // Smooth side movement speed

    private CharacterController controller;
    private int targetLane = 1;           // 0 = left, 1 = middle, 2 = right
    private float verticalVelocity;
    private float gravity = -20f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            controller = gameObject.AddComponent<CharacterController>();
        }
    }

    void Update()
    {
        // ==== CONSTANT FORWARD MOVEMENT ====
        Vector3 move = Vector3.forward * forwardSpeed;

        // ==== LANE INPUT ====
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            targetLane = Mathf.Max(0, targetLane - 1);

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            targetLane = Mathf.Min(2, targetLane + 1);

        // ==== LANE POSITION CALCULATION ====
        float targetX = (targetLane - 1) * laneDistance;

        float deltaX = targetX - transform.position.x;
        float xVelocity = deltaX * laneChangeSpeed;

        move.x = xVelocity;

        // ==== SIMPLE GRAVITY ====
        if (controller.isGrounded)
        {
            verticalVelocity = -1f;
            if (Input.GetKeyDown(KeyCode.Space))
                verticalVelocity = 10f; // jump
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;

        // ==== APPLY FINAL MOVEMENT ====
        controller.Move(move * Time.deltaTime);
    }
}
