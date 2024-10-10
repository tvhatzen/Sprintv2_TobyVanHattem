using UnityEngine;
using Cinemachine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform combatLookAt;

    // Reference to Cinemachine virtual cameras
    public CinemachineVirtualCamera thirdPersonCam;
    public CinemachineVirtualCamera combatCam;
    public CinemachineVirtualCamera topDownCam;

    public CameraStyle currentStyle;
    public enum CameraStyle
    {
        Basic,
        Combat,
        Topdown
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Set the default camera style
        SwitchCameraStyle(CameraStyle.Basic);
    }

    private void Update()
    {
        // Switch styles based on number key input
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CameraStyle.Basic);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchCameraStyle(CameraStyle.Combat);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchCameraStyle(CameraStyle.Topdown);

        // Rotate orientation to follow the player
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // Rotate player object
        if (currentStyle == CameraStyle.Basic || currentStyle == CameraStyle.Topdown)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
        else if (currentStyle == CameraStyle.Combat)
        {
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;
            playerObj.forward = dirToCombatLookAt.normalized;
        }
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        // Reset all camera priorities
        thirdPersonCam.Priority = 10;
        combatCam.Priority = 10;
        topDownCam.Priority = 10;

        // Activate the selected camera by increasing its priority
        if (newStyle == CameraStyle.Basic)
            thirdPersonCam.Priority = 20;  // Third-person camera takes control
        else if (newStyle == CameraStyle.Combat)
            combatCam.Priority = 20;  // Combat camera takes control
        else if (newStyle == CameraStyle.Topdown)
            topDownCam.Priority = 20;  // Top-down camera takes control

        currentStyle = newStyle;
    }
}