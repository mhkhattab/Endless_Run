using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 6f;
    public float laneDistance = 2f;       // distance between lanes
    [Range(0, 2)] public int currentLane = 1;  // 0 = left, 1 = center, 2 = right
    public float laneChangeSmooth = 10f;

    [Header("Jump")]
    public float jumpForce = 7f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private float targetX;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // starting in center lane
        targetX = (currentLane - 1) * laneDistance;
    }

    void Update()
    {
        // ------------------------------------
        // 1. Handle lane input
        // ------------------------------------
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            ChangeLane(-1);

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            ChangeLane(+1);

        // ------------------------------------
        // 2. Grounded check
        // ------------------------------------
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f,
                                     Vector3.down, 
                                     0.2f,
                                     groundMask);

        // ------------------------------------
        // 3. Jump
        // ------------------------------------
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
    }

    void FixedUpdate()
    {
        // ------------------------------------
        // PURE FORWARD MOVEMENT (Z ONLY)
        // ------------------------------------
        float newZ = rb.position.z + forwardSpeed * Time.fixedDeltaTime;

        // ------------------------------------
        // PURE SIDE MOVEMENT (X ONLY)
        // ------------------------------------
        float newX = Mathf.Lerp(rb.position.x, targetX, laneChangeSmooth * Time.fixedDeltaTime);

        // ------------------------------------
        // COMBINE MOVEMENT
        // ------------------------------------
        Vector3 newPos = new Vector3(newX, rb.position.y, newZ);

        rb.MovePosition(newPos);
    }

    void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, 0, 2);
        targetX = (currentLane - 1) * laneDistance;   // only X changes!
    }
}
