using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            if(PlayerInfo.piInstance.currentScene == "Town1")
            {
                Destroy(gameObject);
                Destroy(collision.transform.parent.parent.gameObject);
                PlayerInfo.piInstance.Town1MonsterKilled = true;
                PlayerInfo.piInstance.IncreaseLevel();
            }
            if (PlayerInfo.piInstance.currentScene == "Town2")
            {
                Destroy(gameObject);
                Destroy(collision.transform.parent.parent.gameObject);
                PlayerInfo.piInstance.Town2MonsterKilled = true;
                PlayerInfo.piInstance.IncreaseLevel();
            }
            if (PlayerInfo.piInstance.currentScene == "Town3")
            {
                Destroy(gameObject);
                Destroy(collision.transform.parent.parent.gameObject);
                PlayerInfo.piInstance.Town3MonsterKilled = true;
                PlayerInfo.piInstance.IncreaseLevel();
            }
        }
    }
}
