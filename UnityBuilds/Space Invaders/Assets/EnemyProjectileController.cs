using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyProjectileController : MonoBehaviour
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
        transform.Translate(direction * speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("PlayerShip"))
        {
            gameState.playerHealth--;
            Destroy(gameObject);
            if (gameState.playerHealth == 0) 
            {
                Destroy(collider.gameObject);
                SceneManager.LoadScene("Game Over");
                gameState.enemyShipsLeft = 18;
                gameState.playerHealth = 3;
                gameState.displayScore = gameState.score;
                gameState.score = 0;
            }
        }
        if (collider.gameObject.CompareTag("Boxes"))
        {
            Destroy(gameObject);
        }
    }
}
