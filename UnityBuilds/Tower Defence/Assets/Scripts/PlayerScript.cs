using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private PrefabPool pool;
    private void Awake()
    {
        pool = GameObject.Find("PrefabPool").GetComponent<PrefabPool>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
            pool.numEnemyShipsInScene--;
            if (pool.numEnemyShipsInScene == 0)
            {
                SceneManager.LoadScene("Win");
            }
        }
    }
}
