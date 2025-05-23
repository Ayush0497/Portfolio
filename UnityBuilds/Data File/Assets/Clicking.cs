using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using UnityEngine.SceneManagement;
using TMPro;

public class Clicking : MonoBehaviour
{
    float start;
    int score;
    float time;

    [SerializeField] TextMeshProUGUI countdown;
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI TimeLeft;
    Submit SavedcharacterData;

    private void Start()
    {
        start = 3.0f;
        score = 0;
        time = 10;
        SavedcharacterData = GameObject.FindGameObjectWithTag("gamestate").GetComponent<Submit>();
    }

    private void Update()
    {
        scoreText.text = $"Score: {score.ToString()}";
        if (start > 0.0f)
        {
            start = start - (1.0f * Time.deltaTime);
            countdown.text = Mathf.CeilToInt(start).ToString();
            TimeLeft.text = $"Time Left: {Mathf.CeilToInt(time).ToString()}";
            button.interactable = false;
        }
        if(start <  0.0f)
        {
            countdown.text = "Go..";
            button.interactable = true;
            time = time - Time.deltaTime;
            TimeLeft.text = $"Time Left: {Mathf.CeilToInt(time).ToString()}";
            if(time < 0.0f)
            {
                
                if (SavedcharacterData.characterData == null)
                {
                    SavedcharacterData.currentPlayer.Score = score;
                    Stream stream = File.Open("SaveLoadData.xml", FileMode.Create);
                    XmlSerializer serializer = new XmlSerializer(typeof(CharacterData));
                    serializer.Serialize(stream, SavedcharacterData.currentPlayer);
                    stream.Close();
                }
                else if (SavedcharacterData.characterData.Score < score)
                {
                    SavedcharacterData.currentPlayer.Score = score;
                    Stream stream = File.Open("SaveLoadData.xml", FileMode.Create);
                    XmlSerializer serializer = new XmlSerializer(typeof(CharacterData));
                    serializer.Serialize(stream, SavedcharacterData.currentPlayer);
                    stream.Close();
                }
                SceneManager.LoadScene(0);
                SavedcharacterData.SceneLoaded();
            }
        }
    }

    public void onClick()
    {
        score = score+1;
    }    
}
