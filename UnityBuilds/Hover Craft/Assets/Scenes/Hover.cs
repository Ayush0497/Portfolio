using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField] List<Rigidbody> rbody;
    [SerializeField] float forceAmount = 10.0f;

    private void FixedUpdate()
    {
        foreach(Rigidbody r in rbody)
        {
            r.AddForce(transform.up * Time.deltaTime * forceAmount, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        rbody.Add(other.GetComponentInParent<Rigidbody>());
    }

    private void OnTriggerExit(Collider other)
    {
        rbody.Remove(other.GetComponentInParent<Rigidbody>());
    }
}
