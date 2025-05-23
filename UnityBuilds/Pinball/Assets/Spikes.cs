using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{
    public GameObject ballPreFab;
    public Transform ballSpawnPoint;
    public GameState gameState;

    void Start()
    {
        gameState = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(collider.gameObject);
        StartCoroutine(Wait());
        gameState.lives = gameState.lives - 1;
        if(gameState.lives == 0)
        {
            SaveToDisk();
            SceneManager.LoadScene(2);
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        Instantiate(ballPreFab, ballSpawnPoint.position, ballPreFab.transform.rotation);
    }

    public void SaveToDisk()
    {
        string dataPath = Path.Combine(Application.persistentDataPath, "Lab03.txt");
        string jsonString = JsonUtility.ToJson(gameState);
        Debug.Log("Saving Data to " + Application.persistentDataPath);
        if (!File.Exists(dataPath))
        {
            using (StreamWriter writer = File.CreateText(dataPath))
            {
                writer.WriteLine(jsonString);
            }
        }
        else
        {
            using (StreamWriter writer = File.AppendText(dataPath))
            {
                Debug.Log(jsonString);
                writer.WriteLine(jsonString);
            }
        }
    }
}
