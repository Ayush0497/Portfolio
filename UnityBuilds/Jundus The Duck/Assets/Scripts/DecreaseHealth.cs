using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DecreaseHealth : MonoBehaviour
{
    GameManagerEndless endless;
    GameManagerLevel1 level1;
    JundusInLevel player;

    private void Start()
    {
        endless = GameObject.FindWithTag("GameManager").GetComponent<GameManagerEndless>();
        level1 = GameObject.FindWithTag("GameManager").GetComponent<GameManagerLevel1>();
        if (GameObject.FindWithTag("Player") != null)
        {
            player = GameObject.FindWithTag("Player").GetComponent<JundusInLevel>();
        }
        else
        {
            player = GameObject.FindWithTag("NotPlayer").GetComponent<JundusInLevel>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && endless!= null)
        {
            endless.DecreaseHealth();
            player.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            player.resetPlayer();
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (collision.gameObject.tag == "Player" && level1 != null)
        {
            level1.DecreaseHealth();
            player.GetComponent<SpriteRenderer>().color = Color.red;
            player.resetPlayer();
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }  
    }
}
