using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    public PlayerMovement playerMovementScript; // Reference to the walking script
    public RollingModeController rollingModeScript; // Reference to the rolling script
    public GameObject RightFoot;
    public GameObject LeftFoot;
    public Animator animator; // Animator for the ball

    private bool isRolling = false; // Tracks the current mode
    private Rigidbody rb; // Rigidbody reference for physics interaction
    public float jumpForce = 5f; // Jump force value
    public float delayAfterPeak = 0.5f; // Delay before feet reappear

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rollingModeScript.enabled = false; // Start in walking mode
    }

    void Update()
    {
        // Switch modes when Shift key is pressed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ToggleMode();
        }
    }

    private void ToggleMode()
    {
        isRolling = !isRolling; // Switch the mode state

        if (isRolling)
        {
            TransitionToRollingMode();
        }
        else
        {
            TransitionToWalkingMode();
        }
    }

    private void TransitionToRollingMode()
    {
        // Disable feet or animations related to walking (if any)
        SetFeetVisibility(false);

        if (animator != null)
        {
            animator.enabled = false;
        }

        // Enable Rolling Mode and disable Walking Mode
        rollingModeScript.enabled = true;
        playerMovementScript.enabled = false;
    }

    private void TransitionToWalkingMode()
    {
        // Add a small upward force to avoid floor collision
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Adjust as needed

        // Start the coroutine to show feet with a delay after reaching jump peak
        StartCoroutine(ShowFeetAfterPeak());

        if (animator != null)
        {
            animator.enabled = true;
        }

        // Enable Walking Mode and disable Rolling Mode
        rollingModeScript.enabled = false;
        playerMovementScript.enabled = true;
    }

    // Coroutine to wait for peak and then reveal feet
    private IEnumerator ShowFeetAfterPeak()
    {
        // Wait until the ball reaches the peak of its jump
        yield return new WaitUntil(() => rb.velocity.y <= 0);

        // Optional: Add a small delay after reaching the peak
        yield return new WaitForSeconds(delayAfterPeak);

        // Make the feet visible again
        SetFeetVisibility(true);
    }

    // Helper method to control the visibility of feet (or related objects)
    private void SetFeetVisibility(bool isVisible)
    {
        // Add logic to hide/show feet or related components
        // Example: feetObject.SetActive(isVisible);
        RightFoot.SetActive(isVisible);
        LeftFoot.SetActive(isVisible);
    }
}

