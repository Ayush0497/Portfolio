using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void Start()
    {
        if(this.gameObject.name == "PowerUp1" && PlayerInfo.piInstance.PowerUp1Collected)
        {
            Destroy(gameObject);
        }
        else if (this.gameObject.name == "PowerUp2" && PlayerInfo.piInstance.PowerUp2Collected)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerInfo.piInstance.IncreaseLevel();
            if (gameObject.name == "PowerUp1")
            {
                PlayerInfo.piInstance.PowerUp1Collected = true;
            }
            else if (gameObject.name == "PowerUp2")
            {
                PlayerInfo.piInstance.PowerUp2Collected = true;
            }

            PlayerInfo.piInstance.SaveGame();
            Destroy(gameObject);
        }
    }
}
