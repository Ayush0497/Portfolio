using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTriggerEndless : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "NotPlayer")
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene("GameOverEndless");
        }
        else if (collision.gameObject.tag == "FlyingEnemy")
		{
			Destroy(collision.gameObject);
		}
    }
}
