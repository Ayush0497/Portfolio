using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public int upperRandomRange;
    GameState gameState;

    private void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
    }

    void Update()
    {
        int rando = Random.Range(1, upperRandomRange);
        if (rando == 1)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("WallLeft")|| collider.gameObject.CompareTag("WallRight"))
        {
            gameObject.transform.parent.GetComponent<ParentController>().direction.x *= -1;
            Vector3 position = gameObject.transform.parent.position;
            position.y += -0.5F;
            gameObject.transform.parent.position = position;
        }

        if (collider.gameObject.CompareTag("PlayerShip"))
        {
            Destroy(collider.gameObject);
            SceneManager.LoadScene("Game Over");
            gameState.enemyShipsLeft = 18;
            gameState.playerHealth = 3;
            gameState.displayScore = gameState.score;
            gameState.score = 0;
        }
    }
}
