using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ToolType : Tool
{
    public ToolType(string name, int id, int maxHealth)
    {
        Name = name;
        ID = id;
        Maxhealth = maxHealth;
    }
    public ToolType() { }
}
