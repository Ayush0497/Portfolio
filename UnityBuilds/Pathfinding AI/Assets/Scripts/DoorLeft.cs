using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorLeft : MonoBehaviour
{
    [SerializeField] GameObject node;
    bool open = false;
    bool phase = false;
    [SerializeField] TextMeshProUGUI DoorState;

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            node.GetComponent<Pathnode>().nodeActive = true;
            GetComponentInChildren<MeshRenderer>().enabled = false;
            GetComponentInChildren<Collider>().enabled = false;
            DoorState.text = "Door State: Open";
        }
        else if (phase)
        {
            node.GetComponent<Pathnode>().nodeActive = true;
            GetComponentInChildren<Collider>().enabled = false;
            DoorState.text = "Door State: Can Phase Through";
        }
        else
        {
            node.GetComponent<Pathnode>().nodeActive = false;
            GetComponentInChildren<MeshRenderer>().enabled = true;
            DoorState.text = "Door State: Closed";
        }
    }

    void changeOpen()
    {
        open = true;
        StartCoroutine(ReenableDoorAfterDelay());
    }

    void phaseThrough()
    {
        phase = true;
        StartCoroutine(ReenableDoorAfterDelay());
    }

    IEnumerator ReenableDoorAfterDelay()
    {
        yield return new WaitForSeconds(11f);
        phase = false;
        open = false;
    }
}
