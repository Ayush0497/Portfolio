using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFalling : MonoBehaviour
{
    private Renderer myRenderer;
    Rigidbody2D myRigidbody;
    SoundHub mySoundHub;
    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        mySoundHub = GameObject.Find("SoundHub").GetComponent<SoundHub>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        myRenderer.material.color = Random.ColorHSV();
        StartCoroutine("WaitThenFall");
    }
    IEnumerator WaitThenFall()
    {
        yield return new WaitForSeconds(1);
        myRigidbody.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
