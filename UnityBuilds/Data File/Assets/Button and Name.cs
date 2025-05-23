using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using UnityEngine.SceneManagement;
using TMPro;

public class Submit : MonoBehaviour
{
    public CharacterData characterData;
    public CharacterData currentPlayer;
    [SerializeField] TMPro.TMP_InputField Name;
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI TopScorer;
    [SerializeField] TextMeshProUGUI TopScore;


    private void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("gamestate");
        if (gameObjects.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        SceneLoaded();
    }
    public void SceneLoaded()
    {
        Name.text = "";
        if (File.Exists("SaveLoadData.xml"))
        {
            characterData = new CharacterData();
            Stream stream = File.Open("SaveLoadData.xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(CharacterData));
            characterData = (CharacterData)serializer.Deserialize(stream);
            stream.Close();
            TopScorer.text = $"Top Scorer: {characterData.Name}";
            TopScore.text = $"Top Score: {characterData.Score}";
        }
        else
        {
            TopScorer.text = "Top Scorer: None";
            TopScore.text = "Top Score: 0";
        }

        if (string.IsNullOrEmpty(Name.text))
        {
            button.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (string.IsNullOrEmpty(Name.text))
        {
            button.gameObject.SetActive(false);
        }

        if (!string.IsNullOrEmpty(Name.text) && !button.gameObject.activeInHierarchy)
        {
            button.gameObject.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void LoadNext(int index)
    {
        currentPlayer = new CharacterData();
        currentPlayer.Name = Name.text;
        SceneManager.LoadScene(index);
    }

    //public void start()
    //{
    //    characterData.Name = Name.text;
    //    characterData.Score = 10;
    //    Stream stream = File.Open("SaveLoadData.xml", FileMode.Create);
    //    XmlSerializer serializer = new XmlSerializer(typeof(CharacterData));
    //    serializer.Serialize(stream, characterData);
    //    stream.Close();
    //}
}
