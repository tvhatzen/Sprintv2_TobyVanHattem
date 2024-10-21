using System.Collections;
using UnityEngine;


public class RollingModeController : MonoBehaviour
{
    public float rollSpeed = 10f; // Speed of the ball
    public Transform orientation; // Reference to the camera's orientation

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to the ball
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Get input from the player
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate movement direction based on camera orientation
        Vector3 movement = orientation.forward * moveZ + orientation.right * moveX;
        movement.y = 0f; // Keep movement on the XZ plane

        // Apply force to move the ball in the camera's aligned direction
        rb.AddForce(movement.normalized * rollSpeed, ForceMode.Force);
    }
}
