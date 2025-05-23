using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class MusicAndSFX : MonoBehaviour
{
    [SerializeField] public GameObject musicSlider;
    [SerializeField] public GameObject sfxSlider;

    private void Start()
    {
        musicSlider.GetComponent<Slider>().value = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().volume * 100;
    }
    public void MusicChange()
    {
        GameObject musicSource = GameObject.FindGameObjectWithTag("Music");
        musicSource.GetComponent<AudioSource>().volume = musicSlider.GetComponent<Slider>().value / 100;
    }

    public void SFXChange()
    {
        GameObject[] SFXSource = GameObject.FindGameObjectsWithTag("SFX");
        foreach (GameObject SFX in SFXSource)
        {
            SFX.GetComponent<AudioSource>().volume = sfxSlider.GetComponent<Slider>().value / 100;
        }  
    }
}
