using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{
    SoundHub soundHub;

    private void Awake()
    {
        soundHub = GameObject.Find("SoundHub").GetComponent<SoundHub>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            soundHub.PlayOuchSound();
            SceneManager.LoadScene(1);
        }
    }
}
