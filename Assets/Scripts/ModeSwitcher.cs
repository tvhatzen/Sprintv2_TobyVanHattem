using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    public PlayerMovement playerMovementScript; // Reference to the walking script
    public RollingModeController rollingModeScript; // Reference to the rolling script

    private Animator animator;
    private bool isRolling = false;

    void Start()
    {
        rollingModeScript.enabled = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Switch modes when Shift key is pressed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ToggleMode();
        }
    }

    void ToggleMode()
    {
        isRolling = !isRolling;

        // Play the Curl animation when transitioning
        if (isRolling)
        {
            StartCoroutine(TransitionToRollingMode());
        }
        else
        {
            StartCoroutine(TransitionToWalkingMode());
        }
    }

    IEnumerator TransitionToRollingMode()
    {
        // Play Curl animation to enter rolling mode
        animator.Play("CurlAnim");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Wait for the animation to finish

        // Enable Rolling Mode
        rollingModeScript.enabled = true;
        playerMovementScript.enabled = false;

        // Allow full rotation in rolling mode
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    IEnumerator TransitionToWalkingMode()
    {
        // Add a small jump to avoid hitbox collision with the floor
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 5f, ForceMode.Impulse); // Adjust the value as needed for the jump

        // Play Curl animation in reverse to exit rolling mode
        animator.Play("CurlAnim", 0, 1); // This starts the animation at the end
        animator.speed = -1; // Play animation in reverse
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Wait for the animation to finish

        // Reset animator speed back to normal
        animator.speed = 1;

        // Enable Walking Mode
        rollingModeScript.enabled = false;
        playerMovementScript.enabled = true;

        // Keep the ball upright in walking mode
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
}
