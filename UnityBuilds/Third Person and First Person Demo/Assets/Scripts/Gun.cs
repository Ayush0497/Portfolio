using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] int ammo = 30, maxAmmo = 30;
    [SerializeField] float fireRate = 0.5f, timestamp = -0.5f, fireForce = 20.0f;
    [SerializeField] GameObject bullet, barrelEnd;

    void Fire()
    {
        if (ammo > 0 && Time.time > timestamp + fireRate)
        {
            ammo--;
            timestamp = Time.time;
            GameObject instantiatedObject = Instantiate(bullet, barrelEnd.transform.position, barrelEnd.transform.rotation);
            Physics.IgnoreCollision(instantiatedObject.GetComponentInChildren<Collider>(), gameObject.GetComponentInChildren<Collider>());
            instantiatedObject.GetComponent<Rigidbody>().velocity = barrelEnd.transform.forward * fireForce;
            Events.ReloadStuff.Invoke(ammo);
        }
    }

    void reload()
    {
        ammo = maxAmmo;
        Events.ReloadStuff.Invoke(maxAmmo);
    }
}
