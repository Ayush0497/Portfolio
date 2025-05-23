using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownManager : MonoBehaviour
{
    PlayerController player;
    bool transition = false;
    [SerializeField] GameObject Monster;
    [SerializeField] GameObject Coin;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position + PlayerInfo.piInstance.spawnLocation.normalized * 8.5f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.Fade(false);
        if(PlayerInfo.piInstance.currentScene == "Town1")
        {
            if (PlayerInfo.piInstance.Town1MonsterKilled)
            {
                Monster.SetActive(false);
            }
            if(PlayerInfo.piInstance.Treasure1Collected)
            {
                Coin.SetActive(false);
            }
        }

        if (PlayerInfo.piInstance.currentScene == "Town2")
        {
            if (PlayerInfo.piInstance.Town2MonsterKilled)
            {
                Monster.SetActive(false);
            }
            if (PlayerInfo.piInstance.Treasure2Collected)
            {
                Monster.SetActive(false);
            }
        }

        if (PlayerInfo.piInstance.currentScene == "Town3")
        {
            if (PlayerInfo.piInstance.Town3MonsterKilled)
            {
                Monster.SetActive(false);
            }
            if (PlayerInfo.piInstance.Treasure3Collected)
            {
                Monster.SetActive(false);
            }
        }
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
       if(other.gameObject.tag == "Player")
       {
            PlayerInfo.piInstance.spawnLocation = other.transform.position - transform.position;
            player = other.GetComponent<PlayerController>();
            player.Fade(true);
            transition = true;
            PlayerInfo.piInstance.currentSceneName = "StartingScene";
            PlayerInfo.piInstance.SaveGame();
       }                
    }
}
