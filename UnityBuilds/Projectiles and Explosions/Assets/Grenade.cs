using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class Grenade : MonoBehaviour
{
    Rigidbody2D rb;
    Renderer myRenderer;
    public GameObject grenade;
    internal double timeBetweenLaunchAndCollisionX;
    internal double timeBetweenLaunchAndCollisionY;
    public LaunchProjectile launchProjectile;
    bool hascollided = false;
    public float explosionStrength;
    public float explosionRadius;

    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
        rb = GameObject.FindGameObjectWithTag("Grenade").GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!hascollided)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            timeBetweenLaunchAndCollisionX = (rb.position.x-launchProjectile.initialpositionX)/launchProjectile.velocityX;  //T = S/V where S is change in positions and velocity is constant while in air, using ∆𝐷𝑥 = 𝑉𝑥𝑡 i.e. t = ∆𝐷𝑥/𝑉𝑥𝑡
            timeBetweenLaunchAndCollisionY = (-1 * launchProjectile.velocityY - Math.Sqrt(Math.Pow(launchProjectile.velocityY,2) - 2 * -9.81 * (-1 * (rb.position.y - launchProjectile.initialpositionY))))/-9.81; //using 𝑡 =−𝑉𝑖 ± √𝑉𝑖 * 𝑉𝑖 − 2(𝐴)(−∆𝐷)/𝐴
            rb.velocity = Vector2.zero;
            hascollided = true;
        }

        if(hascollided)
        {
           myRenderer.material.color = Color.red;
           StartCoroutine("WaitThenBoom");
        }
    }

    public void AddExplosionForce()
    {
        Rigidbody2D[] objects = FindObjectsOfType<Rigidbody2D>();
        GameObject ob = GameObject.FindGameObjectWithTag("Grenade");
        Destroy(ob);
        foreach(Rigidbody2D obj in objects)
        {
            if(obj.CompareTag("Walls"))
            {
                float distance = Vector2.Distance(transform.position, obj.transform.position);
                float forceMagnitude = (explosionStrength / distance) * obj.mass;
                Vector2 explosionDirection = (obj.transform.position - transform.position).normalized;
                if (distance <= explosionRadius)
                {
                    obj.AddForce(explosionDirection * forceMagnitude, ForceMode2D.Impulse);
                }
            }
        }
    }
    IEnumerator WaitThenBoom()
    {
        yield return new WaitForSeconds(2);
        AddExplosionForce();
    }
}
