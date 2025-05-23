using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class InventoryData
{
    public List<Item> PlayerInventory = new List<Item>();
    public List<Item> TreasureChest1 = new List<Item>();
    public List<Item> TreasureChest2 = new List<Item>();
    public List<Item> TreasureChest3 = new List<Item>();
    public List<Item> TreasureChest4 = new List<Item>();

    public InventoryData() // Default constructor for initialization
    {
        PlayerInventory = new List<Item>();
        TreasureChest1 = new List<Item>();
        TreasureChest2 = new List<Item>();
        TreasureChest3 = new List<Item>();
        TreasureChest4 = new List<Item>();
    }
    public InventoryData(InventoryManager inventory)
    {
        PlayerInventory = inventory.PlayerInventory;
        TreasureChest1 = inventory.TreasureChest1;
        TreasureChest2 = inventory.TreasureChest2;
        TreasureChest3 = inventory.TreasureChest3;
        TreasureChest4 = inventory.TreasureChest4;
    }
}
