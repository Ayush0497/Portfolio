using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections.Generic;

public class HUDLevel01 : MonoBehaviour
{
    public GameState gameState;
    public TextMeshProUGUI scoreTextLabel;

    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
        scoreTextLabel = GameObject.Find("TextScore").GetComponent<TextMeshProUGUI>();
        gameState.score = 0;
        gameState.lives = 3;
        gameState.totalDropTargets = 6;
        Debug.Log(gameState.userName);
    }

    void Update()
    {
        scoreTextLabel.text = "Score: " + gameState.score + "\n" + "Lives:" + gameState.lives;
    }
    public void loadFromDisk()
    {
        string dataPath = Path.Combine(Application.persistentDataPath, "Lab03.txt");
        Debug.Log("Loading Data From " + Application.persistentDataPath);

        using (StreamReader reader = File.OpenText(dataPath))
        {
            string jsonString = reader.ReadToEnd();
            JsonUtility.FromJsonOverwrite(jsonString, gameState);
        }
    }
}
