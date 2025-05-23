using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    private PrefabPool pool;
    private void Awake()
    {
        pool = GameObject.Find("PrefabPool").GetComponent<PrefabPool>();
    }
   private void OnCollisionEnter2D(Collision2D collision)
   {
        if (collision.collider.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
            pool.numEnemyShipsInScene--;
            if(pool.numEnemyShipsInScene == 0)
            {
                SceneManager.LoadScene("Win");
            }
        }

        if (collision.collider.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
   }
}
