using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] GameObject LoadingPanel;

    private void Start()
    {
		if(LoadingPanel != null)
		{
            LoadingPanel.SetActive(false);
        }
    }
    public void LoadSceneByIndex(int index)
	{
        if (LoadingPanel != null && !LoadingPanel.gameObject.activeInHierarchy)
        {
            LoadingPanel.SetActive(true);
        }
        else
        {
            LoadingPanel.SetActive(false);
        }
        SceneManager.LoadScene(index);
		Time.timeScale = 1.0f;
	}

	public void LoadSceneByName(string sceneName)
	{
        if (LoadingPanel != null)
        {
            LoadingPanel.SetActive(true);
        }
        SceneManager.LoadScene(sceneName);
		Time.timeScale = 1.0f;
	}
}
