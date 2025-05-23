using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] AudioSource AudioSource;
    [SerializeField] ParticleSystem ParticleSystem;
    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            AudioSource.Play();
            ParticleSystem.Play();
            DestroySelf();
        }     
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
