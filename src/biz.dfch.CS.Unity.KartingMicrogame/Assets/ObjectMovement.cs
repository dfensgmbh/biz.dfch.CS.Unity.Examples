using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float turnSpeed = 50f;

    public float range;

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

        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");
        //float xPos = h * range;
        //float yPos = v * range;
        //
        //transform.position = new Vector3(xPos, yPos, 0);

        Debug.Log("GetKeyDown: " + Input.GetKeyDown(KeyCode.KeypadEnter));
        Debug.Log("GetKey: " + Input.GetKey(KeyCode.KeypadEnter));
        Debug.Log("GetKeyUp: " + Input.GetKeyUp(KeyCode.KeypadEnter));

        Debug.Log("GetButtonDown: " + Input.GetButtonDown("Jump"));
        Debug.Log("GetButton: " + Input.GetButton("Jump"));
        Debug.Log("GetButtonUp: " + Input.GetButtonUp("Jump"));

    }
}
