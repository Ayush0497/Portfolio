using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LaunchProjectile : MonoBehaviour
{
    public GameObject projectile;
    public Rigidbody2D rb;
    internal bool shotOnce = false;
    public float angle;
    public float launchVelocity;
    internal float velocityX;
    internal float velocityY;
    internal double initialpositionX;
    internal double initialpositionY;
    private void Awake()
    {
        rb = projectile.GetComponent<Rigidbody2D>();
        initialpositionX = rb.position.x;
        initialpositionY = rb.position.y;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !shotOnce)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            LaunchTheProjectile();
            shotOnce = true;
        }
    }
    void LaunchTheProjectile()
    {
        float radianAngle = angle * Mathf.Deg2Rad;
        velocityX = launchVelocity * Mathf.Cos(radianAngle);
        velocityY = launchVelocity * Mathf.Sin(radianAngle);
        rb.AddForce(new Vector2(velocityX, velocityY), ForceMode2D.Impulse);
    }
}

