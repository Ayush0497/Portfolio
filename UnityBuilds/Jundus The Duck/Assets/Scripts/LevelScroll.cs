using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScroll : MonoBehaviour
{
    [SerializeField] float ScrollingSpeed;
    void Update()
    {
        transform.position = new Vector3(transform.position.x - ScrollingSpeed*Time.deltaTime, 0, 0); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "End")
        {
            ScrollingSpeed = 0;
        }
    }
}
