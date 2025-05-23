using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] Rigidbody cubeBody;
    [SerializeField] Rigidbody PlayerBody;
    private Vector3 playerVelocity;
    private Vector3 cubeVelocity;

    private void FixedUpdate()
    {
        playerVelocity = PlayerBody.velocity;
        cubeVelocity = cubeBody.velocity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Cube")
        {
            cubeBody.velocity = -cubeVelocity;
        }
        if(collision.gameObject.tag == "Player")
        {
            PlayerBody.velocity = -playerVelocity;
        }
    }
}
