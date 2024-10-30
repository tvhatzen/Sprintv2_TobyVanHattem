using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    public PlayerMovement playerMovementScript;
    public RollingModeController rollingModeScript;
    public GameObject RightFoot;
    public GameObject LeftFoot;
    public Animator animator;
    public bool isRolling { get; private set; }

    private Rigidbody rb;
    public float jumpForce = 5f;
    public float delayAfterPeak = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rollingModeScript.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ToggleMode();
        }
    }

    private void ToggleMode()
    {
        isRolling = !isRolling;

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
        SetFeetVisibility(false);
        if (animator != null) animator.enabled = false;

        rollingModeScript.enabled = true;
        playerMovementScript.enabled = false;

        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = Vector3.zero;
    }

    private void TransitionToWalkingMode()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        StartCoroutine(ShowFeetAfterPeak());

        if (animator != null) animator.enabled = true;

        rollingModeScript.enabled = false;
        playerMovementScript.enabled = true;

        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationZ |
                         RigidbodyConstraints.FreezeRotationY;

        rb.velocity = Vector3.zero;
    }

    private IEnumerator ShowFeetAfterPeak()
    {
        yield return new WaitUntil(() => rb.velocity.y <= 0);
        yield return new WaitForSeconds(delayAfterPeak);
        SetFeetVisibility(true);
    }

    private void SetFeetVisibility(bool isVisible)
    {
        RightFoot.SetActive(isVisible);
        LeftFoot.SetActive(isVisible);
    }
}
