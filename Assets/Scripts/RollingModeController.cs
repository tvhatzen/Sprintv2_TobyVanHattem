using System.Collections;
using UnityEngine;

public class RollingModeController : MonoBehaviour
{
    public float rollSpeed = 10f;
    public Transform orientation;

    Camera mainCamera;
    private Rigidbody rb;

    void Start()
    {
        mainCamera = Camera.main;
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

        Vector3 cameraForward = mainCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 cameraRight = mainCamera.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        Vector3 movement = cameraForward * moveZ + cameraRight * moveX;
        movement.y = 0f;

        rb.AddForce(movement.normalized * rollSpeed, ForceMode.Force);

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

