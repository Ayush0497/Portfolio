using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    SoundHub soundHub;
    Tracking gameState;
    private int previousCoins;
    private void Start()
    {
        soundHub = GameObject.Find("SoundHub").GetComponent<SoundHub>();
        gameState = GameObject.FindGameObjectWithTag("Gamestate").GetComponent<Tracking>();
        previousCoins = gameState.coins;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameState.coins--;
            Destroy(this.gameObject);
            soundHub.PlayCoinSound();
        }
    }
}
