using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Document : MonoBehaviour
{
    [SerializeField] Pathfinder BlueSpy;
    [SerializeField] RedSpy RedSpy;
    private void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == "blue")
        {
            this.gameObject.SetActive(false);
            if(BlueSpy != null)
            {
                BlueSpy.UpdateDocument();
            }
        }

        if (collider.gameObject.tag == "red")
        {
            Destroy(gameObject);
            if (RedSpy != null)
            {
                RedSpy.UpdateDocument();
            }
        }
    }
}
