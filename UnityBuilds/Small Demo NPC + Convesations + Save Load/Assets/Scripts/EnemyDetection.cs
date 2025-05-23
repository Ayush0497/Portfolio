using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject barrelLocation;
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private float fireRate = 1f; // Time between shots

    private float nextFireTime = 0f; // Timer to track when the enemy can shoot again

    private void Update()
    {
        if (target != null)
        {
            // Rotate towards the player
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // Check if enough time has passed to shoot again
            if (Time.time >= nextFireTime)
            {
                shoot();
                nextFireTime = Time.time + fireRate;
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

    private void shoot()
    {
        GameObject spawnedBullet = Instantiate(bullet, barrelLocation.transform.position, barrelLocation.transform.rotation);
        Rigidbody rb = spawnedBullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(barrelLocation.transform.forward * bulletForce, ForceMode.Impulse);
        }
        StartCoroutine(DestroyAfter1Second(spawnedBullet));
    }

    private IEnumerator DestroyAfter1Second(GameObject bullet)
    {
        yield return new WaitForSeconds(1);
        Destroy(bullet);
    }    
}