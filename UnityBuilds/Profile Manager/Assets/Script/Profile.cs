using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Profile // Saves the profile name, preferences, and highscore
{
    public string name;
    public int colorIndex;
    public int VehicleIndex;
    public float score;
    public GhostDataContainer ghost;

    public Profile()
    {
        name = "New Profile";
        colorIndex = 0;
        VehicleIndex = 0;
        score = 0;
        ghost = null;
    }
    public Profile(string changeName, int changeColor, int changeVehicle, float changeScore, GhostDataContainer ghostData)
    {
        name = changeName;
        colorIndex = changeColor;
        VehicleIndex = changeVehicle;
        score = changeScore;
        ghost = ghostData;
    }
    public string GetName()
    {
        return name;
    }
    public void SetName(string changeName)
    {
        name = changeName;
    }
    public int GetColor()
    {
        return colorIndex;
    }
    public void SetColor(int changeColor)
    {
        colorIndex = changeColor;
    }
    public int GetShape()
    {
        return VehicleIndex;
    }
    public void SetVehicle(int changeShape)
    {
        VehicleIndex = changeShape;
    }
    public float GetScore()
    {
        return score;
    }
    public void SetScore(float value)
    {
        score = value;
    }

    public GhostDataContainer GetGhostData()
    {
        return ghost;
    }

    public void SetGhostData(GhostDataContainer ghostData)
    {
        ghost = ghostData;
    }
}
