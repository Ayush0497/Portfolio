using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarsCounter : MonoBehaviour
{
    [SerializeField] GameManagerLevel1 gameManager;
    [SerializeField] GameObject Star2;
    [SerializeField] GameObject Star3;
    private static StarsCounter instance;

    [SerializeField] int health;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Update()
    {
        if ((Star2 == null))
        {
            Star2 = GameObject.FindGameObjectWithTag("Star2");
        }

        if ((Star3 == null))
        {
            Star3 = GameObject.FindGameObjectWithTag("Star3");
        }

        if(gameManager == null && SceneManager.GetActiveScene().name == "Level1")
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerLevel1>();
        }

        if (gameManager != null)
        {
            health = gameManager.myGameData.Health;
        }

        if (health == 1 && Star2 != null && Star3!=null)
        {
            Star2.gameObject.SetActive(false);
            Star3.gameObject.SetActive(false);
        }

        if (health == 2 && Star2 != null && Star3 != null)
        {
            Star2.gameObject.SetActive(true);
            Star3.gameObject.SetActive(false);
        }

        if (SceneManager.GetActiveScene().name != "Level1" && SceneManager.GetActiveScene().name != "Level1Victory")
        {
            Destroy(this.gameObject);
        }
    }
}
