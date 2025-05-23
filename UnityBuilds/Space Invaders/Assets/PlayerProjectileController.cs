using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProjectileController : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    GameState gameState;
    private void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
    }
    void Update()
    {
        transform.Translate(direction*speed*Time.deltaTime,Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Scene scene = SceneManager.GetActiveScene();
        if (collider.gameObject.CompareTag("EnemyShip"))
        {
                Destroy(collider.gameObject);
                gameState.enemyShipsLeft--;
                gameState.score++;
                if (gameState.score > gameState.highScore)
                {
                    gameState.highScore = gameState.score;
                }
                Destroy(gameObject);
                if (scene.name == "Level 1" && gameState.enemyShipsLeft == 0) 
                {
                SceneManager.LoadScene("Level 2");
                gameState.enemyShipsLeft = 19;
                }
                else if(scene.name == "Level 2" && gameState.enemyShipsLeft  == 0)
                {
                SceneManager.LoadScene("Win");
                gameState.enemyShipsLeft = 18;
                gameState.playerHealth = 3;
                gameState.displayScore = gameState.score;
                gameState.score = 0;
                }
        }
    }
}
