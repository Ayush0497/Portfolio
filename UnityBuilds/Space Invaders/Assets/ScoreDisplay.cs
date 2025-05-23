using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreDisplay : MonoBehaviour
{
    GameState gameState;
    public void Awake()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
    }

    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Main Menu")
        {
            this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "\nHigh Score: " + gameState.highScore.ToString();
        }

        if(scene.name == "Level 1" || scene.name == "Level 2")
        {
            this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "HP: " + gameState.playerHealth.ToString() + "\nEnemies Left: " + gameState.enemyShipsLeft.ToString() + "\nHigh Score: " + gameState.highScore.ToString() + "\nScore: " + gameState.score.ToString();
        }

        if(scene.name == "Win" || scene.name == "Game Over")
        {
            this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "\nScore: " + gameState.displayScore.ToString() + "\nHigh Score: " + gameState.highScore.ToString();
        }
    }
}
