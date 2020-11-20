using UnityEngine;
using System.Collections;

public class YeetAway : MonoBehaviour
{
    void OnMouseDown()
    {
        var rigidbody = GetComponent<Rigidbody>();

        rigidbody.AddForce(-transform.forward * 1000f);
        rigidbody.AddForce(transform.up * 500f);
        rigidbody.useGravity = true;
    }
}
