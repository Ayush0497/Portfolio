using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public int upperRandomRange;

    public float speed;
    public Vector3 direction;

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * direction);
        int rando = Random.Range(1, upperRandomRange);
        if (rando == 1)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("WallLeft") || collider.gameObject.CompareTag("WallRight"))
        {
            speed *= -1;
            Vector3 position = transform.position;
            position.y += -0.5F;
            gameObject.transform.position = position;
        }

        if (collider.gameObject.CompareTag("PlayerShip"))
        {
            Destroy(collider.gameObject);
        }
    }
}
