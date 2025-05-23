using System.Collections;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
[System.Serializable]
public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo piInstance = null;
    public Vector3 spawnLocation, colliderObjectLocation;
    public string currentScene;
    public string currentSceneName;
    public int playerHP;
    public bool Town1MonsterKilled;
    public bool Town2MonsterKilled;
    public bool Town3MonsterKilled;
    public bool Treasure1Collected;
    public bool Treasure2Collected;
    public bool Treasure3Collected;

    private void Awake()
    {
        if (piInstance == null)
        {
            piInstance = this;
            this.playerHP = 100;
            DontDestroyOnLoad(gameObject);
        }
        else if (piInstance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        if(piInstance == null) return; // Ensure Singleton is not null

        // Specify the path to save in the project root
        string path = Path.Combine(Application.dataPath, "../SaveFile.xml"); // Go up one level from the Assets folder
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerInformation));

        // Create the FileStream to serialize the data
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, new PlayerInformation(piInstance)); // Use piInstance
        }

        Debug.Log("Game Saved: " + path);
    }

    public void LoadGame()
    {
        // Specify the path to load from the project root
        string path = Path.Combine(Application.dataPath, "../SaveFile.xml"); // Go up one level from the Assets folder

        if (File.Exists(path))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PlayerInformation));
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                PlayerInformation data = (PlayerInformation)serializer.Deserialize(stream);
                // Restore data to PlayerInfo
                spawnLocation = data.GetSpawnLocation();
                colliderObjectLocation = data.GetColliderLocation();
                currentScene = data.currentScene;
                currentSceneName = "StartingScene";
                playerHP = data.playerHP;
                Town1MonsterKilled = data.Town1MonsterKilled;
                Town2MonsterKilled = data.Town2MonsterKilled;
                Town3MonsterKilled = data.Town3MonsterKilled;
                Treasure1Collected = data.Treasure1Collected;
                Treasure2Collected = data.Treasure2Collected;
                Treasure3Collected = data.Treasure3Collected;
            }
        }
        else
        {
            Debug.LogWarning("Save file not found at: " + path);
        }
    }

    public void InitializeForNewGame()
    {
        spawnLocation = Vector3.zero; // Default spawn location (can be adjusted based on your game's design)
        colliderObjectLocation = Vector3.zero; // Default collider location
        currentScene = "StartingScene"; // Set to the starting scene name
        currentSceneName = "StartingScene";
        playerHP = 100; // Default player health (adjust as needed)
        Town1MonsterKilled = false; // Monster not killed at the start
        Town2MonsterKilled = false; // Monster not killed at the start
        Town3MonsterKilled = false; // Monster not killed at the start
        Treasure1Collected = false; // Treasure not collected at the start
        Treasure2Collected = false; // Treasure not collected at the start
        Treasure3Collected = false; // Treasure not collected at the start
    }
}
