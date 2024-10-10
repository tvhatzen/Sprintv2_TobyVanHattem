using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    void LateUpdate()
    {
        // Locks the rotation to the world axis
        transform.rotation = Quaternion.identity;
    }
}