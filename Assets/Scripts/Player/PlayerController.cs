using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float forwardSpeed = 8f;
    public float laneDistance = 2f; // distance between lanes
    public float lateralSmooth = 10f;
    public int startLane = 1; // 0-left,1-middle,2-right
    public float jumpForce = 6f;
    public float gravity = -20f;
    public LayerMask groundMask;
    public float groundCheckDistance = 0.2f;
    public Transform groundCheck;

    [Header("References")]
    public Animator animator;

    CharacterController controller;
    int currentLane = 1;
    Vector3 verticalVelocity;
    float desiredX;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        currentLane = startLane;
        UpdateDesiredX();
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
        UpdateAnimations();
    }

    void HandleInput()
    {
        // Keyboard / Arrow input
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            ChangeLane(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            ChangeLane(+1);
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")) && IsGrounded())
        {
            Jump();
        }

        // Simple touch swipe (vertical only for jump, horizontal for lane change)
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Ended)
            {
                Vector2 delta = t.position - t.rawPosition; // fallback if needed
            }
            // You can replace this with a proper swipe detection if desired
        }
    }

    void ChangeLane(int direction)
    {
        currentLane = Mathf.Clamp(currentLane + direction, 0, 2);
        UpdateDesiredX();
    }

    void UpdateDesiredX()
    {
        // center lane = 0 => x = (lane -1)*laneDistance
        desiredX = (currentLane - 1) * laneDistance;
    }

    void HandleMovement()
    {
        // Forward
        Vector3 move = transform.forward * forwardSpeed;

        // Lateral - smooth to target x
        Vector3 worldTarget = new Vector3(desiredX, 0, 0);
        Vector3 localPos = transform.InverseTransformPoint(transform.position);
        float newX = Mathf.Lerp(localPos.x, desiredX, Time.deltaTime * lateralSmooth);
        // convert back to world movement (difference)
        float lateral = newX - localPos.x;

        move += transform.right * lateral * (1f / Time.deltaTime); // adjust to world speed

        // Vertical
        if (IsGrounded() && verticalVelocity.y < 0f)
            verticalVelocity.y = -2f; // small grounded stick

        verticalVelocity.y += gravity * Time.deltaTime;
        move += verticalVelocity;

        controller.Move(move * Time.deltaTime);
    }

    void Jump()
    {
        verticalVelocity.y = jumpForce;
        animator?.SetTrigger("Jump");
    }

    bool IsGrounded()
    {
        if (groundCheck == null)
        {
            // fallback - character controller isGrounded
            return controller.isGrounded;
        }
        return Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
    }

    void UpdateAnimations()
    {
        if (animator == null) return;
        animator.SetFloat("ForwardSpeed", forwardSpeed);
        animator.SetBool("IsGrounded", IsGrounded());
        animator.SetFloat("VerticalVelocity", verticalVelocity.y);
    }

    // Reset position and physics for restart
    public void ResetPlayer(Vector3 startPosition, int lane = 1)
    {
        transform.position = startPosition;
        currentLane = lane;
        UpdateDesiredX();
        verticalVelocity = Vector3.zero;
    }
}
