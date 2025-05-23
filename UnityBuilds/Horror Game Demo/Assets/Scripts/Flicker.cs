using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public Light flickeringLight; 
    public float minIntensity = 0f;
    public float maxIntensity = 10f; 
    public float flickerSpeed = 0.1f;

    private void Start()
    {
        // Ensure the light component is assigned
        if (flickeringLight == null)
        {
            flickeringLight = GetComponent<Light>();
        }
        StartCoroutine(Flicker1());
    }

    private IEnumerator Flicker1()
    {
        while (true)
        {
            // Set the light intensity to a random value between min and max
            flickeringLight.intensity = Random.Range(minIntensity, maxIntensity);
            // Wait for the specified flicker speed before the next update
            yield return new WaitForSeconds(flickerSpeed);
        }
    }
}
