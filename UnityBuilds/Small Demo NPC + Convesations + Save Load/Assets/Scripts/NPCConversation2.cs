using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCConversation2 : MonoBehaviour
{
    [SerializeField] GameObject conversationCanvas;
    [SerializeField] GameObject UIPanel;
    private void Start()
    {
        if (PlayerInfo.piInstance.hasMetNPC2)
        {
            Destroy(gameObject);
        }
        conversationCanvas.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !PlayerInfo.piInstance.hasMetNPC2)
        {
            if (UIPanel == null)
            {
                UIPanel = GameObject.Find("UI");
                UIPanel.SetActive(false);
            }
            conversationCanvas.SetActive(true);
        }
    }

    public void ExitConversation()
    {
        GameObject.Find("UIAudioSource").GetComponent<AudioSource>().Play();
        conversationCanvas.SetActive(false);
        UIPanel.SetActive(true);
        PlayerInfo.piInstance.hasMetNPC2 = true;
        PlayerInfo.piInstance.SaveGame();
        Destroy(gameObject);
    }
}
