using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] GameObject UIAudioSource;

    private void Start()
    {
        UIAudioSource = GameObject.FindGameObjectWithTag("SFX");
    }
    public void RedJack()
    {
        if (UIAudioSource != null)
        {
            UIAudioSource.GetComponent<AudioSource>().Play();
        }
        PlayerInfo.piInstance.InitializeForNewGame();
        PlayerInfo.piInstance.CharacterName = "RedJack";
        PlayerInfo.piInstance.SaveGame();
        SceneManager.LoadScene("Overworld");
    }

    public void BlueJack()
    {
        if (UIAudioSource != null)
        {
            UIAudioSource.GetComponent<AudioSource>().Play();
        }
        PlayerInfo.piInstance.InitializeForNewGame();
        PlayerInfo.piInstance.CharacterName = "BlueJack";
        PlayerInfo.piInstance.SaveGame();
        SceneManager.LoadScene("Overworld");
    }

}
