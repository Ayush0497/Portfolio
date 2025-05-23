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
        AudioSource click = GameObject.Find("UIAudioSource").GetComponent<AudioSource>();
        click.Play();
        PlayerInfo.piInstance.SaveGame();
        Application.Quit();
    }
}
