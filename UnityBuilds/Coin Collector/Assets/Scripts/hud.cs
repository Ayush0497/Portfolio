using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class hud : MonoBehaviour
{
    public Tracking gameState;
    public TextMeshProUGUI scoreTextLabel;

    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("Gamestate").GetComponent<Tracking>();
        scoreTextLabel = GameObject.Find("TextScore").GetComponent<TextMeshProUGUI>();
        gameState.coins = 17;
    }

    void Update()
    {
        scoreTextLabel.text = "Coins Left: " + gameState.coins + "\n" + "Objective: Collect all coins and reach the Castle to complete the level";
    }
}
