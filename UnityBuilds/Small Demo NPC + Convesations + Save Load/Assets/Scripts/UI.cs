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
    [SerializeField] TextMeshProUGUI LevelText;
    [SerializeField] TextMeshProUGUI FireRate;


    private void Update()
    {
        if(PlayerInfo.piInstance.Level == 0)
        {
            FireRate.text = $"FireRate: 2.0 seconds";
        }
        else if (PlayerInfo.piInstance.Level == 1)
        {
            FireRate.text = $"FireRate: 1.5 seconds";
        }
        else if (PlayerInfo.piInstance.Level == 2)
        {
            FireRate.text = $"FireRate: 1.0 seconds";
        }
        else if (PlayerInfo.piInstance.Level == 3)
        {
            FireRate.text = $"FireRate: 0.5 seconds";
        }
        else if (PlayerInfo.piInstance.Level == 4)
        {
            FireRate.text = $"FireRate: 0.25 seconds";
        }
        else if (PlayerInfo.piInstance.Level == 5)
        {
            FireRate.text = $"FireRate: 0.10 seconds";
        }

        if(PlayerInfo.piInstance.Level == 5)
        {
            LevelText.text = $"Level: 5 (Max)";
        }
        else
        {
            LevelText.text = $"Level: {(PlayerInfo.piInstance.Level)}";
        }  
        HP.text = $"HP: {(PlayerInfo.piInstance.playerHP)}";
        if(PlayerInfo.piInstance.Treasure1Collected)
        {
            Treasure1.text = "Town 1 Key: Collected";
        }
        else
        {
            Treasure1.text = "Town 1 Key: Not Collected";
        }
        if (PlayerInfo.piInstance.Treasure2Collected)
        {
            Treasure2.text = "Town 2 Key: Collected";
        }
        else
        {
            Treasure2.text = "Town 2 Key: Not Collected";
        }
        if (PlayerInfo.piInstance.Treasure3Collected)
        {
            Treasure3.text = "Town 3 Key: Collected";
        }
        else
        {
            Treasure3.text = "Town 3 Key: Not Collected";
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
