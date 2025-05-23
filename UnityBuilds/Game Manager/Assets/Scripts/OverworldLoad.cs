using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldLoad : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        if (!SceneManager.GetSceneByName("Player").isLoaded)
        {
            SceneManager.LoadScene("Player",LoadSceneMode.Additive);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (PlayerInfo.piInstance.spawnLocation != Vector3.zero)
        {
            player.transform.position = PlayerInfo.piInstance.colliderObjectLocation + PlayerInfo.piInstance.spawnLocation.normalized * 3.7f;

        }
        else
        {
            player.transform.position = transform.position;
        }
        player.GetComponent<PlayerController>().Fade(false);
    }
}
