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
    [SerializeField] GameObject Panel;
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
        PlayerInfo.piInstance.InitializeForNewGame();
        PlayerInfo.piInstance.SaveGame();
        SceneManager.LoadScene("Overworld");       
    }

    public void Load()
    {
        PlayerInfo.piInstance.LoadGame(); // Call the LoadGame method directly
        SceneManager.LoadScene("Overworld");
    }
}
