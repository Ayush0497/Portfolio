using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;

public class HUDMainMenu : MonoBehaviour
{
    public GameState gameState;
    public TextMeshProUGUI topScoreTextLabel;
    public List<string> scores = new List<string>();

    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
        topScoreTextLabel = GameObject.Find("TextScore").GetComponent<TextMeshProUGUI>();
        loadFromDisk();
        Debug.Log(gameState.userName);
        gameState.userName = topScoreTextLabel.text;
    }

    void Update()
    {
        string allScores = "Top Scores:\n";
        int count = Mathf.Min(10, scores.Count);

        for (int i = 0; i < count; i++)
        {
            allScores += scores[i] + "\n";
        }
        topScoreTextLabel.text = allScores;
    }

    public void loadFromDisk()
    {
        string dataPath = Path.Combine(Application.persistentDataPath, "Lab03.txt");
        Debug.Log("Loading Data From " + Application.persistentDataPath);
        using (StreamReader reader = File.OpenText(dataPath))
        {
            while(!reader.EndOfStream) 
            {
                string jsonString = reader.ReadLine();
                JsonUtility.FromJsonOverwrite(jsonString, gameState);
                scores.Add(gameState.userName.ToString()+ ": " + gameState.score.ToString());
            } 
        }
        scores = scores.OrderByDescending(s => int.Parse(s.Split(':')[1].Trim())).ToList();
    }
}
