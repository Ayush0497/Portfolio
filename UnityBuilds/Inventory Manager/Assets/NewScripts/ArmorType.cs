using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ArmorType : Armor
{
    public ArmorType(string name, int id, int defence)
    {
        Name = name;
        ID = id;
        Defense = defence;
    }
    public ArmorType() { }
}
