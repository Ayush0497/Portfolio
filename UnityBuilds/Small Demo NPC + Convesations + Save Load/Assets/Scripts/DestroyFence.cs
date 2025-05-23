using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyFence : MonoBehaviour
{
    private void Update()
    {
        if(PlayerInfo.piInstance.Treasure1Collected && PlayerInfo.piInstance.Treasure2Collected && PlayerInfo.piInstance.Treasure3Collected)
        {
            gameObject.SetActive(false);
        }
    }
}
