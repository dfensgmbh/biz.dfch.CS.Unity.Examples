using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearyInterpolate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var resultMathf = Mathf.Lerp(5f, 15f, 0.6f);
        
        var from = new Vector3(4f, 10f, 6f);
        var to = new Vector3(10f, 12f, 9f);
        var resultVector3 = Vector3.Lerp(from, to, 0.6f);

        Debug.Log("Mathf.Lerp: " + resultVector3);
        Debug.Log("Vector3.Lerp: " + resultMathf);

    }
}
