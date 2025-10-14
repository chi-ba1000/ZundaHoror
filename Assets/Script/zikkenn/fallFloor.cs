using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class fallfloor : MonoBehaviour
{
    Rigidbody rb;
    float timeRide;
    public float fallTime = 3;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
    /*
    void OnTriggerStay(Collider collider)
    {
        Debug.Log("wwwww");
        rb.isKinematic = false;
    }
    */

    void OnCollisionStay(Collision collision)
    {
        timeRide += Time.deltaTime;
        Debug.Log(timeRide);
        //Debug.Log("PutOn");
        if (timeRide > fallTime)
        {
            rb.isKinematic = false;
        }
    }
    void OnCollisionExit(Collision other)
    {
        Debug.Log("Exit");
    }   

    
    /*
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.gameObject.name);
    }
    */
}


