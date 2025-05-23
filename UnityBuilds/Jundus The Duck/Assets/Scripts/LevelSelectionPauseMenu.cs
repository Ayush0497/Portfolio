using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionPauseMenu : MonoBehaviour
{
    [SerializeField] GameObject PauseMenuPanel;

    private void Start()
    {
        PauseMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenuPanel.activeInHierarchy)
            {
                Time.timeScale = 1.0f;
                PauseMenuPanel.SetActive(false);
            }
            else
            {
                Time.timeScale = 0.0f;
                PauseMenuPanel.SetActive(true);
            }
        }
    }

    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
