using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] int sceneNumber;
    [SerializeField] GameObject LoadingPanel;

    private void Start()
    {
        LoadingPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LoadingPanel.SetActive(true);
            SceneManager.LoadScene(sceneNumber);
        }
    }
}
