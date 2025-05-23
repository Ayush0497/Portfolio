using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button NewGame;
    [SerializeField] Button LoadGame;
    [SerializeField] GameObject UIAudioSource;

    void Start()
    {
        if (File.Exists("SaveFile.xml"))
        {
            LoadGame.interactable = true;
        }
        else
        {
            LoadGame.interactable = false;
        }
    }

    public void New()
    {
        if (UIAudioSource != null)
        {
            UIAudioSource.GetComponent<AudioSource>().Play();
        }
        else
        {
            GameObject.Find("UIAudioSource").GetComponent<AudioSource>().Play();
        }
        SceneManager.LoadScene("CharacterSelection");  
    }

    public void Load()
    {
        if (UIAudioSource != null)
        {
            UIAudioSource.GetComponent<AudioSource>().Play();
        }
        else
        {
            GameObject.Find("UIAudioSource").GetComponent<AudioSource>().Play();
        }
        PlayerInfo.piInstance.LoadGame(); // Call the LoadGame method directly
        SceneManager.LoadScene("Overworld");  
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
