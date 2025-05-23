using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDestroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
