using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBullet : MonoBehaviour
{
    [SerializeField] ParticleSystem ParticleSystemm;
    [SerializeField] AudioSource CollisionAudio;
    void Start()
    {
        Invoke("DestroySelf", 3.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
        {
            // Use the first contact point of the collision for the particle system
            ContactPoint contact = collision.contacts[0];

            // Instantiate and play the particle system at the collision point
            ParticleSystem particleInstance = Instantiate(ParticleSystemm, contact.point, Quaternion.identity);
            particleInstance.Play();

            // Destroy the particle system after it finishes playing
            Destroy(particleInstance.gameObject, particleInstance.main.duration);

            // Instantiate and play the collision audio at the collision point
            AudioSource audioInstance = Instantiate(CollisionAudio, contact.point, Quaternion.identity);
            audioInstance.Play();

            // Destroy the audio instance after it finishes playing
            Destroy(audioInstance.gameObject, audioInstance.clip.length);
        }

        if (collision.gameObject.tag == "Player")
        {

            Health objectHealth = collision.gameObject.GetComponent<Health>();
            if (objectHealth != null)
            {
                objectHealth.applyDamage(30.0f);
            }
        }
        DestroySelf();
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
