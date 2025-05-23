using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public AdvancePlayerAI AdvancePlayerAI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AdvancePlayerAI.movementSpeed = 10;
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            StartCoroutine(ResetSpeedAfterDelay(3f));
        }
    }

    private IEnumerator ResetSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        AdvancePlayerAI.movementSpeed = 5; // Reset the movement speed
        Destroy(this.gameObject);
    }
}