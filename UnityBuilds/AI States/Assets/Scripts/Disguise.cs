using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disguise : MonoBehaviour
{
    public GameObject x;
    public Material current;
    public Material newColor;
    public AdvancePlayerAI AdvancePlayerAI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            x.tag = "NotPlayer";
            x.GetComponent<Renderer>().material.color = newColor.color;
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            StartCoroutine(ResetSpeedAfterDelay(3f));
        }
    }

    private IEnumerator ResetSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        x.GetComponent<Renderer>().material.color = current.color;
        AdvancePlayerAI.mystate = AdvancePlayerAI.state.RoamingAround;
        x.tag = "Player"; // Reset the movement speed
        Destroy(this.gameObject);
    }
}
