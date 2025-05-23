using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(PlayerInfo.piInstance.currentScene == "Town1" && PlayerInfo.piInstance.Town1MonsterKilled)
            {
                PlayerInfo.piInstance.Treasure1Collected = true;
                gameObject.SetActive(false);
            }

            if (PlayerInfo.piInstance.currentScene == "Town2" && PlayerInfo.piInstance.Town2MonsterKilled)
            {
                PlayerInfo.piInstance.Treasure2Collected = true;
                gameObject.SetActive(false);
            }

            if (PlayerInfo.piInstance.currentScene == "Town3" && PlayerInfo.piInstance.Town3MonsterKilled)
            {
                PlayerInfo.piInstance.Treasure3Collected = true;
                gameObject.SetActive(false);
            }
        }
    }
}
