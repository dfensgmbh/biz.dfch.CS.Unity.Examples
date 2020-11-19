using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float turnSpeed = 50f;

    void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.K))
        {
            transform.Translate(-Vector3.forward * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.J))
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.L))
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }
    }
}
