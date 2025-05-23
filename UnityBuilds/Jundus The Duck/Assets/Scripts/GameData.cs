using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameData
{
    public int Health;
    public float Score;

    public int GetHealth()
    {
        return Health;
    }

    public void SetHealth()
    {
        Health = Health - 1;
    }

    public GameData()
    {
        Health = 3;
        Score = 0;
    }
}
