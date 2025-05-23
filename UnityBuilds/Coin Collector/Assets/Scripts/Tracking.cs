using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour
{
    public int coins = 17;
    public bool won = false;
    private void Awake()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Gamestate");
        if (gameObjects.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
