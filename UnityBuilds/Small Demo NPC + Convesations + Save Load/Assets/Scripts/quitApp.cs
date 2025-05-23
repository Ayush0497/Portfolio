using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitApp : MonoBehaviour
{
    public void QuitApp()
    {
        AudioSource click = GameObject.Find("UIAudioSource").GetComponent<AudioSource>();
        click.Play();
        Application.Quit();
    }
}
