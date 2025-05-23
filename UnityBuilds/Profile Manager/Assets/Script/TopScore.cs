using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TopScore // Saves the name and score for the top five all time scorers.
{
    public string name;
    public float score;


    public TopScore()
    {
        name = "Empty";
        score = 100000.0f;
    }
    public TopScore(string changeName, int changeScore)
    {
        name = changeName;
        score = changeScore;
    }
    public string GetName()
    {
        return name;
    }
    public void SetName(string changeName)
    {
        name = changeName;
    }
    public float GetScore()
    {
        return score;
    }
    public void SetScore(float value)
    {
        score = value;
    }
}
