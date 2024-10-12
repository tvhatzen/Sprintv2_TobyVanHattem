using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingModeController : MonoBehaviour
{
    public float rollSpeed = 10f;
    public float maxSpeed = 15f; // Maximum rolling speed
    public float rotationSpeed = 100f;
    public Transform orientation; // Reference to the camera's orientation, like in PlayerMovement

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HandleMovement();
        SpeedControl();
    }

    private void HandleMovement()
    {
        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate the movement direction based on camera orientation
        Vector3 moveDirection = orientation.forward * moveZ + orientation.right * moveX;
        moveDirection.y = 0; // Make sure the movement is only on the XZ plane

        // Apply force to roll the ball in the camera-aligned direction
        rb.AddForce(moveDirection.normalized * rollSpeed);

        // Optional: Rolling mode rotation, if needed (can be removed if not needed)
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        rb.transform.Rotate(0, rotation, 0);
    }
    private void SpeedControl()
    {
        // Get the flat velocity (ignore vertical velocity)
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // If the velocity exceeds the maxSpeed, clamp it
        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}