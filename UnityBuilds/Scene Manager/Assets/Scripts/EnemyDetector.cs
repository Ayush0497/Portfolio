using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject barrelLocation;
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private float fireRate = 1f; // Time between shots

    private float nextFireTime = 0f; // Timer to track when the enemy can shoot again
    private bool isShooting = false;  // Flag to check if shooting is in progress

    private void Update()
    {
        if (target != null)
        {
            // Rotate towards the player
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(-direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // Check if enough time has passed to shoot again
            if (Time.time >= nextFireTime && !isShooting)
            {
                StartCoroutine(ShootWithDelay());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
        }
    }

    private IEnumerator ShootWithDelay()
    {
        isShooting = true; // Set the flag to true to indicate shooting is in progress

        // Delay before shooting (can adjust as needed)
        yield return new WaitForSeconds(0.5f); // Wait for half a second after rotating

        shoot(); // Call the shoot method
        nextFireTime = Time.time + fireRate; // Set the next fire time

        isShooting = false; // Reset the flag after shooting is done
    }

    private void shoot()
    {
        int numberOfBullets = 5; // Number of bullets to spawn
        float spreadAngle = 15f; // Maximum angle for spread

        for (int i = 0; i < numberOfBullets; i++)
        {
            // Calculate the angle for each bullet
            float angle = Random.Range(-spreadAngle, spreadAngle);
            Quaternion bulletRotation = barrelLocation.transform.rotation * Quaternion.Euler(0, angle, 0);

            // Instantiate the bullet
            GameObject spawnedBullet = Instantiate(bullet, barrelLocation.transform.position, bulletRotation);
            Rigidbody rb = spawnedBullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Add force in the direction the bullet is facing
                rb.AddForce(bulletRotation * -Vector3.forward * bulletForce, ForceMode.Impulse);
            }
        }
    }
}
