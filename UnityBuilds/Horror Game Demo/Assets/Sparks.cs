using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparks : MonoBehaviour
{
    [SerializeField] private ParticleSystem sparkSystem;
    [SerializeField] private AudioSource audioSource;

    private float interval = 3.0f;

    private void Start()
    {
        StartCoroutine(EmitSparksRandomly());
    }

    private IEnumerator EmitSparksRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            // Generate a random value between 0 and 100
            float randomValue = Random.Range(0.0f, 100);

            if (randomValue <= 50) // Adjust the percentage as needed
            {
                sparkSystem.Emit(20); // Emit sparks
                audioSource.Play();
            }
        }
    }
}