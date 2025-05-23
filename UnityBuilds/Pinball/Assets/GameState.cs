using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public int score;
    public int lives = 3;
    public int totalDropTargets = 6;
    public string userName;
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("GameState");
        if(gameObjects.Length > 1 )
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void ReadStringInput(string input)
    {
        userName = input.Trim();
    }
}