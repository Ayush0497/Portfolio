using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameState : MonoBehaviour
{
    public int score = 0;
    public int playerHealth = 3;
    public int enemyShipsLeft = 18;
    public int highScore = 0;
    public int displayScore = 0;
    private void Awake()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("GameState");
        if(gameObjects.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
