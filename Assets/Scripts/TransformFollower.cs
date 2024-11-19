using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFollower : MonoBehaviour
{
    public float followSpeed = 10;

    public Transform followTarget;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, followTarget.position, Time.deltaTime * followSpeed);
    }
}
