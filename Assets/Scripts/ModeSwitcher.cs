using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    public PlayerMovement playerMovementScript; // Reference to the walking script
    public RollingModeController rollingModeScript; // Reference to the rolling script

    private bool isRolling = false;

    void Start()
    {
        // Ensure that rolling mode is initially disabled
        rollingModeScript.enabled = false;
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

        // Enable/Disable the appropriate scripts
        playerMovementScript.enabled = !isRolling;
        rollingModeScript.enabled = isRolling;

        // If switching to rolling mode, reset the rotation so the ball rolls properly
        if (isRolling)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        else
        {
            // Keep the ball upright in walking mode
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }
}