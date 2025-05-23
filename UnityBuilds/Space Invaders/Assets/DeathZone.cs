using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    GameState gameState;
    bool gameOverTriggered = false;
    private void Awake()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(collider.gameObject);
        if(collider.gameObject.CompareTag("EnemyShip"))
        {
            if(!gameOverTriggered)
            {
                SceneManager.LoadScene("Game Over");
                gameState.enemyShipsLeft = 18;
                gameState.playerHealth = 3;
                gameState.displayScore = gameState.score;
                gameState.score = 0;
                gameOverTriggered = true;
            }
        }
    }
}
