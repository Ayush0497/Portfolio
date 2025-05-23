using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plunger : MonoBehaviour
{
    public Transform plungerLowestPoint;
    public float force;  
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= plungerLowestPoint.position.y && Input.GetAxis("Fire1")!=0)
        {
            transform.Translate(0, Input.GetAxis("Fire1") * Time.deltaTime * -2, 0 , Space.Self);
        }

        if(Input.GetKeyUp(KeyCode.Space) ) 
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
        }
    }
}
