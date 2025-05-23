using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] GameObject cannonBall, barrelEnd, cannonBarrel;
    [SerializeField] float fireForce = 60.0f;
    [SerializeField] AudioSource firing, colliding;
    [SerializeField] ParticleSystem particles;
    private Vector3 initialBarrelPosition;
    private void Start()
    {
        initialBarrelPosition = cannonBarrel.transform.localPosition;
    }
    private void Update()
    {
        if(Input.GetMouseButton(0) && !firing.isPlaying && !colliding.isPlaying)
        {
            particles.Play();
            firing.Play();
            GameObject instantiatedObject = Instantiate(cannonBall, barrelEnd.transform.position, barrelEnd.transform.rotation);
            instantiatedObject.GetComponent<Rigidbody>().velocity = barrelEnd.transform.forward * fireForce;
            cannonBarrel.transform.localPosition = new Vector3(initialBarrelPosition.x, initialBarrelPosition.y, initialBarrelPosition.z-1.0f);
            StartCoroutine(RecoilEffect());
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private IEnumerator RecoilEffect()
    {
        yield return new WaitForSeconds(0.1f);
        cannonBarrel.transform.localPosition = initialBarrelPosition;
    }
}
