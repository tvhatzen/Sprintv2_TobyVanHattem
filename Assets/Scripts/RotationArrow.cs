using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    public Transform mainBall;  // Reference to the main rolling ball

    void Update()
    {
        // Get the velocity of the main ball
        Vector3 ballVelocity = mainBall.GetComponent<Rigidbody>().velocity;

        // Check if the ball is moving to avoid dividing by zero
        if (ballVelocity.magnitude > 0.1f)
        {
            // Rotate the second ball to point in the direction of the velocity (ignoring Y-axis)
            Vector3 direction = new Vector3(ballVelocity.x, 0, ballVelocity.z);
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    // Draw an arrow gizmo to show the direction the second ball is pointing
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 10);  // Arrow length can be adjusted
        Gizmos.DrawSphere(transform.position + transform.forward * 10
            , 0.1f);  // Optional arrowhead
    }
}
