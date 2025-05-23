using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] Pathfinder BlueSpy;
    [SerializeField] RedSpy RedSpy;
    private void OnTriggerEnter(Collider collider)
    {
        
        if(collider.gameObject.tag == "blue")
        {
            this.gameObject.SetActive(false);
            Events.OpenDoor.Invoke();
            if (BlueSpy != null)
            {
                BlueSpy.UpdateKey();
            }
        }
        if (collider.gameObject.tag == "red")
        {
            this.gameObject.SetActive(false);
            Events.phaseThrough.Invoke();
            if (RedSpy != null)
            {
                RedSpy.UpdateKey();
            }
        }
    }
}
