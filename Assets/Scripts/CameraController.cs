using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform CameraRotationBall;  // Reference to the second ball (gizmo direction)
    public float distance = 10.0f;   // Distance behind the second ball
    public float height = 5.0f;      // Height of the camera from the second ball
    public float smoothSpeed = 0.125f;  // Smoothing factor for the camera movement

    void LateUpdate()
    {
        // Ensure CameraRotationBall is assigned
        if (CameraRotationBall != null)
        {
            // Calculate the desired position behind the second ball
            Vector3 desiredPosition = CameraRotationBall.position - CameraRotationBall.forward * distance + Vector3.up * height;

            // Smoothly move the camera to the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Make the camera look in the direction the second ball is facing
            transform.LookAt(CameraRotationBall);
        }
        else
        {
            Debug.LogWarning("No secondBall assigned to the camera controller!");
        }
    }
}
