using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WeaponType : Weapon
{
    public WeaponType(string name, int id, int damage)
    {
        Name = name;
        ID = id;
        Damage = damage;
    }
    public WeaponType() { }
}
