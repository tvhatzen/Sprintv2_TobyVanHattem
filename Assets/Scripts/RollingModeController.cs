using System.Collections;
using UnityEngine;

public class RollingModeController : MonoBehaviour
{
    public float rollSpeed = 10f;
    public Transform orientation;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = orientation.forward * moveZ + orientation.right * moveX;
        movement.y = 0f;

        rb.AddForce(movement.normalized * rollSpeed, ForceMode.Acceleration);

        LimitRollingSpeed();
    }

    private void LimitRollingSpeed()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVelocity.magnitude > rollSpeed)
        {
            flatVelocity = flatVelocity.normalized * rollSpeed;
            rb.velocity = new Vector3(flatVelocity.x, rb.velocity.y, flatVelocity.z);

        }
    }
}

