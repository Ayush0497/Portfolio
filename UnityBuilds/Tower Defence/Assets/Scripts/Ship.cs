using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Transform target;
    public float speed;
    void Update()
    {
        //move
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
        //rotate
        Vector3 moveDirection = transform.position - target.position;
        if(moveDirection!=Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x)*Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.RotateAround(transform.position, transform.forward, 180f);
        }
    }
}
