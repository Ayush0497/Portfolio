using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerInfo.piInstance.playerHP > 0)
        {
            PlayerInfo.piInstance.playerHP = PlayerInfo.piInstance.playerHP - 20;
            Destroy(gameObject);
        }
    }
}
