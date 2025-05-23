using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HP;
    [SerializeField] TextMeshProUGUI Treasure1;
    [SerializeField] TextMeshProUGUI Treasure2;
    [SerializeField] TextMeshProUGUI Treasure3;
    [SerializeField] TextMeshProUGUI Enemy1;
    [SerializeField] TextMeshProUGUI Enemy2;
    [SerializeField] TextMeshProUGUI Enemy3;


    private void Update()
    {
        HP.text = $"HP: {(PlayerInfo.piInstance.playerHP)}";
        if(PlayerInfo.piInstance.Treasure1Collected)
        {
            Treasure1.text = "Town 1 Treasure: Collected";
        }
        else
        {
            Treasure1.text = "Town 1 Treasure: Not Collected";
        }
        if (PlayerInfo.piInstance.Treasure2Collected)
        {
            Treasure2.text = "Town 2 Treasure: Collected";
        }
        else
        {
            Treasure2.text = "Town 2 Treasure: Not Collected";
        }
        if (PlayerInfo.piInstance.Treasure3Collected)
        {
            Treasure3.text = "Town 3 Treasure: Collected";
        }
        else
        {
            Treasure3.text = "Town 3 Treasure: Not Collected";
        }
        if (PlayerInfo.piInstance.Town1MonsterKilled)
        {
            Enemy1.text = "Town 1 Enemy: Dead";
        }
        else
        {
            Enemy1.text = "Town 1 Enemy: Alive";
        }
        if (PlayerInfo.piInstance.Town2MonsterKilled)
        {
            Enemy2.text = "Town 2 Enemy: Dead";
        }
        else
        {
            Enemy2.text = "Town 3 Enemy: Alive";
        }
        if (PlayerInfo.piInstance.Town3MonsterKilled)
        {
            Enemy3.text = "Town 3 Enemy: Dead";
        }
        else
        {
            Enemy3.text = "Town 3 Enemy: Alive";
        }
    }
}
