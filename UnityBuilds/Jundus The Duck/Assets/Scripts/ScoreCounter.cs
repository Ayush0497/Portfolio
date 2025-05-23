using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] GameManagerEndless gameManager;
    [SerializeField] float Score;
    private static ScoreCounter instance;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI PauseMenuScoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Update()
    {
        if(gameManager!=  null)
        {
            Score = gameManager.myGameData.Score;
        }
        
        if(gameManager==null && GameObject.FindGameObjectWithTag("GameManager")  != null)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerEndless>();
        }

        if (GameObject.FindGameObjectWithTag("PauseMenuScoreText") != null)
        {
            PauseMenuScoreText = GameObject.FindGameObjectWithTag("PauseMenuScoreText").GetComponent<TextMeshProUGUI>();
        }

        if (SceneManager.GetActiveScene().name != "Endless" && SceneManager.GetActiveScene().name != "GameOverEndless")
        {
            Destroy(this.gameObject);
        }

        if(SceneManager.GetActiveScene().name == "GameOverEndless")
        {
            ScoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
            ScoreText.text = $"{Score:f0}";
        }

        if (PauseMenuScoreText != null)
        {
            PauseMenuScoreText.text = $"{Score:f0}";
        }
    }
}
