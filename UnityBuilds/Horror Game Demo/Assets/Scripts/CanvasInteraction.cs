using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMovement : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Cursor.visible = true;
            player.BroadcastMessage("SwitchPosition", SendMessageOptions.DontRequireReceiver);
        }
    }
}
