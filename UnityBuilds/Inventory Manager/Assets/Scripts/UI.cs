using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Damage;
    [SerializeField] TextMeshProUGUI Armor;
    [SerializeField] TextMeshProUGUI MaxHP;


    private void Update()
    {
        WeaponType weapon = InventoryManager.Instance.PlayerInventory[0] as WeaponType;
        ArmorType armor = InventoryManager.Instance.PlayerInventory[1] as ArmorType;
        ToolType tool = InventoryManager.Instance.PlayerInventory[2] as ToolType;
        Damage.text = $"Damage: {weapon.Damage}";
        Armor.text = $"Armor: {armor.Defense}";
        MaxHP.text = $"MaxHP: {tool.Maxhealth}";
    }
}
