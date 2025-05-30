﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownManager : MonoBehaviour
{
    PlayerController player;
    bool transition = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position + PlayerInfo.piInstance.spawnLocation.normalized * 8.5f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.Fade(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (transition && !player.Fading())
        {
            SceneManager.LoadScene("Overworld", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(PlayerInfo.piInstance.currentScene);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        PlayerInfo.piInstance.spawnLocation = other.transform.position - transform.position;
        player = other.GetComponent<PlayerController>();
        player.Fade(true);
        transition = true;        
    }
}
