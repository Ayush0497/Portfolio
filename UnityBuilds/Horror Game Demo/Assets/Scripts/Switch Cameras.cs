using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCameras : MonoBehaviour
{
    [SerializeField] GameObject cam1;
    [SerializeField] GameObject cam2;
    [SerializeField] ParticleSystem ParticleSystem;
    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] GameObject player;
    [SerializeField] GameObject creature;
    [SerializeField] AudioSource Shatter;
    [SerializeField] AudioSource Smoke;
    [SerializeField] AudioSource Click;

    private void Start()
    {
        cam2.SetActive(false);
        ParticleSystem.Stop();
    }
    public void SwitchCamera1()
    {
        Click.Play();
        cam2.SetActive(false);
        cam1.SetActive(true);
    }

    public void SwitchCamera2()
    {
        cam1.SetActive(false);
        cam2.SetActive(true);
        button1.interactable = false;
        button2.interactable = false;
        if(Click != null)
        {
            Click.Play();
        }
        if(Smoke != null)
        {
            Smoke.Play();
        }
        if(Shatter != null)
        {
            Shatter.Play();
        }
        ParticleSystem.Play();
        player.BroadcastMessage("StartAnimation", SendMessageOptions.DontRequireReceiver);
        StartCoroutine(SpawnCreature());
    }

    IEnumerator SpawnCreature()
    {
        yield return new WaitForSeconds(4f);
        creature.BroadcastMessage("spawnAndMove", SendMessageOptions.DontRequireReceiver);
    }
}
