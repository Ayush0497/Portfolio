using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerLevel1 : MonoBehaviour
{
    public GameData myGameData;
    [SerializeField] GameObject Health1;
    [SerializeField] GameObject Health2;
    [SerializeField] GameObject Health3;
    [SerializeField] GameObject PausePanel;

    private void Start()
    {
        myGameData = new GameData();
        Health1.gameObject.SetActive(true);
        Health2.gameObject.SetActive(true);
        Health3.gameObject.SetActive(true);
        PausePanel.gameObject.SetActive(false);
    }


    private void Update()
    {
        if(myGameData.Health == 3)
        {
            Health1.gameObject.SetActive(true);
            Health2.gameObject.SetActive(true);
            Health3.gameObject.SetActive(true);
        }
        else if(myGameData.Health == 2)
        {
            Health1.gameObject.SetActive(true);
            Health2.gameObject.SetActive(true);
            Health3.gameObject.SetActive(false);
        }
        else if(myGameData.Health == 1)
        {
            Health1.gameObject.SetActive(true);
            Health2.gameObject.SetActive(false);
            Health3.gameObject.SetActive(false);
        }

        if(myGameData.Health == 0)
        {
            Health1.gameObject.SetActive(false);
            Health2.gameObject.SetActive(false);
            Health3.gameObject.SetActive(false);
            SceneManager.LoadScene("GameOverLevel1");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(PausePanel.activeInHierarchy)
            {
                Time.timeScale = 1.0f;
                PausePanel.SetActive(false);
            }
            else
            {
                Time.timeScale = 0.0f;
                PausePanel.SetActive(true);
            }
        }
    }
    public void DecreaseHealth()
    {
        myGameData.SetHealth();
    }

    public void closePauseMenu()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void Quit()
    {
        Application.Quit();
    }
}
