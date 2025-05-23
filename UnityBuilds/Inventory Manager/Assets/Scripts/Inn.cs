using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inn : MonoBehaviour
{
    [SerializeField] GameObject text;

    private void Start()
    {
        text.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //PlayerInfo.piInstance.currentSceneName = "StartingScene";
        PlayerInfo.piInstance.playerHP = 100;
        PlayerInfo.piInstance.Town1MonsterKilled = false;
        PlayerInfo.piInstance.Town2MonsterKilled = false;
        PlayerInfo.piInstance.Town3MonsterKilled = false;
        PlayerInfo.piInstance.SaveGame();
        text.SetActive(true);
    }
}
