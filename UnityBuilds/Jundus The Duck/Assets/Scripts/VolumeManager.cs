using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public float MasterVolume;
    AudioSource[] audioSources;
    void Start()
    {
        MasterVolume = 0.5f;
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        audioSources = GameObject.FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
           source.volume = MasterVolume;
        }
    }
}
