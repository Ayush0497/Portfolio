using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : MonoBehaviour
{
    [SerializeField] float fireRate = 0.30f, timestamp = -0.30f, fireForce = 20.0f;
    [SerializeField] GameObject bullet, barrelEnd;
    [SerializeField] AudioSource BulletAudio;
    [SerializeField] GameObject AudioListenerr;
    [SerializeField] ParticleSystem ParticleSystemm;
    void fire()
    {
        AudioListenerr.transform.position = gameObject.transform.position;
        AudioListenerr.transform.rotation = Quaternion.identity;
        if (Time.time > timestamp + fireRate && Time.timeScale == 1.0f)
        {
            if (BulletAudio != null && ParticleSystemm != null)
            {
                BulletAudio.Play();
                ParticleSystemm.Play();
            }
            timestamp = Time.time;
            GameObject instantiatedObject = Instantiate(bullet, barrelEnd.transform.position, barrelEnd.transform.rotation);
            Physics.IgnoreCollision(instantiatedObject.GetComponentInChildren<Collider>(), gameObject.GetComponentInChildren<Collider>());
            instantiatedObject.GetComponent<Rigidbody>().velocity = barrelEnd.transform.forward * fireForce;
        }
    }
}
