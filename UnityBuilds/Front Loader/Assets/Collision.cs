using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Balls")
        {
            Rigidbody rb = other.transform.GetComponent<Rigidbody>();
            GameObject vehicle = GameObject.FindWithTag("Vehicle");
            //rb.freezeRotation = true;
            rb.transform.SetParent(vehicle.transform);
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Balls")
        {
            Rigidbody rb = other.transform.GetComponent<Rigidbody>();
            rb.freezeRotation = false;
            rb.transform.SetParent(null);
        }
    }
}
