using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverMessage : MonoBehaviour
{
    public Tracking gameState;
    public TextMeshProUGUI scoreTextLabel;

    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("Gamestate").GetComponent<Tracking>();
        scoreTextLabel = GameObject.Find("TextScore").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(gameState.won)
        {
            scoreTextLabel.text = "You Won, Click on the Button Below to Start Again";
        }
        else
        {
            scoreTextLabel.text = "Ooops!!!! You Lose, Click on the Button Below to Start Again";
        }
    }
}
