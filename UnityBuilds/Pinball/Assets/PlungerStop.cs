using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlungerStop : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.name== "Plunger")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
