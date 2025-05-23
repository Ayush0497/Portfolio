using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndExit : MonoBehaviour
{
    [SerializeField] GameObject button;
    private void Update()
    {
        if (PlayerInfo.piInstance.currentSceneName == "StartingScene")
        {
            button.SetActive(true);
        }
        else
        {
            button.SetActive(false);
        }
    }
    public void SaveandExit()
    {   
        PlayerInfo.piInstance.SaveGame();
        Application.Quit();
    }
}
