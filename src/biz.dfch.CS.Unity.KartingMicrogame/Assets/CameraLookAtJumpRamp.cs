using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraLookAtJumpRamp : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.LookAt(target);
        }
    }
}
