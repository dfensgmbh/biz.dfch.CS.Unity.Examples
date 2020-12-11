using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 1, 0);
        cube.AddComponent<CubeBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
