using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<Renderer>().material.color = Color.red;
        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
        }*/
    }
}
