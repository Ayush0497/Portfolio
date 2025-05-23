using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Treasure : MonoBehaviour
{
    [SerializeField] PlayerController Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        GameObject obj = GameObject.FindGameObjectWithTag("InventoryManager");
        if (obj != null)
        {
            InventoryManager manager = obj.GetComponent<InventoryManager>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.TreasurePanel.SetActive(true);
            if (this.gameObject.tag == "TreasureChest1")
            {
                MyEvents.ChangeChest.Invoke("TreasureChest1");
            }
            if (this.gameObject.tag == "TreasureChest2")
            {
                MyEvents.ChangeChest.Invoke("TreasureChest2");
            }
            if (this.gameObject.tag == "TreasureChest3")
            {
                MyEvents.ChangeChest.Invoke("TreasureChest3");
            }
            if (this.gameObject.tag == "TreasureChest4")
            {
                MyEvents.ChangeChest.Invoke("TreasureChest4");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.TreasurePanel.SetActive(false);
        }
    }
}
