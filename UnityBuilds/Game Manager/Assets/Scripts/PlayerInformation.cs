using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInformation
{
    public float spawnX, spawnY, spawnZ;
    public float colliderX, colliderY, colliderZ;
    public string currentScene;
    public string currentSceneName;
    public int playerHP;
    public bool Town1MonsterKilled;
    public bool Town2MonsterKilled;
    public bool Town3MonsterKilled;
    public bool Treasure1Collected;
    public bool Treasure2Collected;
    public bool Treasure3Collected;

    // Default constructor required for XML Serialization
    public PlayerInformation() { }

    public PlayerInformation(PlayerInfo player)
    {
        spawnX = player.spawnLocation.x;
        spawnY = player.spawnLocation.y;
        spawnZ = player.spawnLocation.z;

        colliderX = player.colliderObjectLocation.x;
        colliderY = player.colliderObjectLocation.y;
        colliderZ = player.colliderObjectLocation.z;

        currentScene = player.currentScene;
        currentSceneName = player.currentSceneName;
        playerHP = player.playerHP;
        Town1MonsterKilled = player.Town1MonsterKilled;
        Town2MonsterKilled = player.Town2MonsterKilled;
        Town3MonsterKilled = player.Town3MonsterKilled;
        Treasure1Collected = player.Treasure1Collected;
        Treasure2Collected = player.Treasure2Collected;
        Treasure3Collected = player.Treasure3Collected;
    }

    public Vector3 GetSpawnLocation() => new Vector3(spawnX, spawnY, spawnZ);
    public Vector3 GetColliderLocation() => new Vector3(colliderX, colliderY, colliderZ);
}
