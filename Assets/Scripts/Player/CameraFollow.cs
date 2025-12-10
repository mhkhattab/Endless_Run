using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;          // The player
    public Vector3 offset = new Vector3(0, 4, -8);
    public float smoothSpeed = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = target.position + offset;
        Vector3 smooth = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
        transform.position = smooth;

        transform.LookAt(target);
    }
}
