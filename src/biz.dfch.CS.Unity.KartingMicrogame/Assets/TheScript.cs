using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheScript : MonoBehaviour
{
    public GameObject GameObject;

    private AnotherScript anotherScript;
    private OtherScript otherScript;
    //private Renderer renderer = new Renderer();

    void Awake()
    {
        anotherScript = GetComponent<AnotherScript>();
        //otherScript = gameObject.GetComponent<OtherScript>();
        //renderer = gameObject.GetComponent<Renderer>();
    }

    void Start()
    {
        Debug.Log("The player's score is " + anotherScript.Score);
        //Debug.Log("The player has died at " + otherScript.Vector3);
        //Debug.Log("Renderer: " + renderer.enabled);
    }
}
