using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTargets : MonoBehaviour
{
    public GameState gameState;

    private void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameState.score = gameState.score + 10;
        gameState.totalDropTargets--;
        if(gameState.totalDropTargets == 0 )
        {
            gameState.lives = gameState.lives + 1;
        }
        Destroy(gameObject);
    }
}
