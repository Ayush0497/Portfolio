using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SpaceshipController : MonoBehaviour
{
    public float speed;
    private bool reachedMaxLeft;
    private bool reachedMaxRight;
    protected bool canShoot = true;
    protected float shotTimer = 1;
    public float secondsBetweenShots;
    public GameObject projectilePrefab;

    void Update()
    {
        float xTranslation = speed * Time.deltaTime * Input.GetAxis("Horizontal");
        if (xTranslation < 0.0f && !reachedMaxLeft)
        {
            transform.Translate(xTranslation, 0, 0);
            reachedMaxRight = false;
        }
        if (xTranslation > 0.0f && !reachedMaxRight)
        {
            transform.Translate(xTranslation, 0, 0);
            reachedMaxLeft = false;
        }
        shotTimer -= Time.deltaTime;
        if (shotTimer < 0)
        {
            canShoot = true;
        }
        if ((Input.GetButtonDown("Fire1")) && canShoot)
        {
            canShoot = false;
            shotTimer = 1;
            StartCoroutine(ShootDelay());
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.position = transform.position;
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(secondsBetweenShots);
        canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.CompareTag("WallLeft"))
        {
            reachedMaxLeft = true;
        }
        if (collider.CompareTag("WallRight"))
        {
            reachedMaxRight = true;
        }
    }
}
