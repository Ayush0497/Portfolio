using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    VolumeManager volumeManager;

    private void Start()
    {
        volumeManager = GameObject.FindObjectOfType<VolumeManager>();
        if(volumeManager != null )
        {
            volumeSlider.value = volumeManager.MasterVolume;
        }
    }

    public void onChange()
    {
        if(volumeManager != null)
        {
            volumeManager.MasterVolume = volumeSlider.value;
        }   
    }
}
