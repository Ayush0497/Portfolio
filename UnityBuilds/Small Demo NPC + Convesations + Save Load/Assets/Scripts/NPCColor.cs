using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCColor : MonoBehaviour
{
    private void Update()
    {
        if(PlayerInfo.piInstance.CharacterName == "BlueJack")
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}
