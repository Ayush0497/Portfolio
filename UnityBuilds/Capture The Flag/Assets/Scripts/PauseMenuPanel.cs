using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;

public class PauseMenuPanel : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] public GameObject firstButton;

    private void Start()
    {
        Panel.SetActive(false);
    }

    public void quitGame(int i)
    {
        SceneManager.LoadScene(i);
        Time.timeScale = 1.0f;
    }

    public void Continue()
    {
        Panel.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1.0f;
        ResumeAllAudio();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            if (Panel.activeInHierarchy)
            {
                Panel.SetActive(false);
                Time.timeScale = 1.0f;
                Cursor.visible = false;
                ResumeAllAudio();
            }
            else
            {
                PauseAllAudio();
                Time.timeScale = 0.0f;
                Cursor.visible = true;

                // Ensure the panel is activated after other settings are applied
                Panel.SetActive(true);

                // Set selected button for navigation
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(firstButton);
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
