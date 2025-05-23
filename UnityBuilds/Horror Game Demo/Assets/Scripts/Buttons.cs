using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] private AudioSource backgroundMusic;

    private void Start()
    {
        Panel.SetActive(false);
    }
    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
        Time.timeScale = 1.0f;
    }
    public void quitGame()
    {
        Application.Quit();
    }

    public void Continue()
    {
        Panel.SetActive(false);
        Time.timeScale = 1.0f;
        ResumeAllAudio();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Panel.activeInHierarchy)
            {
                Panel.SetActive(false);
                Time.timeScale = 1.0f;
                Cursor.visible = false;
                ResumeAllAudio();
            }
            else
            {
                Panel.SetActive(true);
                Cursor.visible = true;
                Time.timeScale = 0.0f;
                PauseAllAudio();
            }           
        }
    }

    private void PauseAllAudio()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();
            }
        }
    }

    private void ResumeAllAudio()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.UnPause();
            }
        }
    }
}
