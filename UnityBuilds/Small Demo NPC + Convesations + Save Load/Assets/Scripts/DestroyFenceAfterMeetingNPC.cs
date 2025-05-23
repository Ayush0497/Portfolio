using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFenceAfterMeetingNPC : MonoBehaviour
{
    private void Update()
    {
        if(PlayerInfo.piInstance.hasMetNPC)
        {
            gameObject.SetActive(false);
        }
    }
}
