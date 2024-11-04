using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public ModeSwitcher modeSwitcher;
    public float rotationSpeed;

    void Update()
    {
        if (!modeSwitcher.isRolling)
        {
            Vector3 viewDir = player.position - new Vector3(transform.position.x, transform.position.y, transform.position.z);
            //orientation.forward = viewDir.normalized;

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
            {
              //  playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        }
        else
        {

        }
    }
}

