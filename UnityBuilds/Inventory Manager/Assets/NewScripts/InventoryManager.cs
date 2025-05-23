using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
[System.Serializable]
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance = null;
    [SerializeField] public List<Item> PlayerInventory;
    [SerializeField] public List<Item> TreasureChest1;
    [SerializeField] public List<Item> TreasureChest2;
    [SerializeField] public List<Item> TreasureChest3;
    [SerializeField] public List<Item> TreasureChest4;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PlayerInventory = new List<Item>();
            TreasureChest1 = new List<Item>();
            TreasureChest2 = new List<Item>();
            TreasureChest3 = new List<Item>();
            TreasureChest4 = new List<Item>();

            PlayerInventory.Add(new WeaponType("Weapon1", 1, 10));
            PlayerInventory.Add(new ArmorType("Armor1", 2, 50));
            PlayerInventory.Add(new ToolType("Tool1", 3, 110));


            TreasureChest1.Add(new WeaponType("Weapon2", 4, 50));
            TreasureChest1.Add(new ArmorType("Armor2", 5, 150));
            TreasureChest1.Add(new ToolType("Tool2", 6, 150));

            TreasureChest2.Add(new WeaponType("Weapon3", 7, 20));
            TreasureChest2.Add(new ArmorType("Armor3", 8, 75));
            TreasureChest2.Add(new ToolType("Tool3", 9, 120));

            TreasureChest3.Add(new WeaponType("Weapon4", 10, 30));
            TreasureChest3.Add(new ArmorType("Armor4", 11, 100));
            TreasureChest3.Add(new ToolType("Tool4", 12, 130));

            TreasureChest4.Add(new WeaponType("Weapon5", 13, 40));
            TreasureChest4.Add(new ArmorType("Armor5", 14, 125));
            TreasureChest4.Add(new ToolType("Tool5", 15, 140));
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        MyEvents.Swap.AddListener(SwapTreasureChestItem);
    }

    public void SwapTreasureChestItem(int index, string treasureChest)
    {
        if (treasureChest == "TreasureChest1")
        {
            Item temp = TreasureChest1[index];
            TreasureChest1[index] = PlayerInventory[index];
            PlayerInventory[index] = temp;
        }
        else if (treasureChest == "TreasureChest2")
        {
            Item temp = TreasureChest2[index];
            TreasureChest2[index] = PlayerInventory[index];
            PlayerInventory[index] = temp;
        }

        else if (treasureChest == "TreasureChest3")
        {
            Item temp = TreasureChest3[index];
            TreasureChest3[index] = PlayerInventory[index];
            PlayerInventory[index] = temp;
        }
        else if (treasureChest == "TreasureChest4")
        {
            Item temp = TreasureChest4[index];
            TreasureChest4[index] = PlayerInventory[index];
            PlayerInventory[index] = temp;
        }
    }

    public void SaveGame()
    {
        if (Instance == null) return; // Ensure Singleton is not null

        // Specify the path to save in the project root
        string path = Path.Combine(Application.dataPath, "../SaveFileInventory.xml"); // Go up one level from the Assets folder
        XmlSerializer serializer = new XmlSerializer(typeof(InventoryData));

        // Create the FileStream to serialize the data
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, new InventoryData(Instance)); // Use piInstance
        }

        Debug.Log("Game Saved: " + path);
    }

    public void LoadGame()
    {
        // Specify the path to load from the project root
        string path = Path.Combine(Application.dataPath, "../SaveFileInventory.xml"); // Go up one level from the Assets folder

        if (File.Exists(path))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(InventoryData));
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                InventoryData data = (InventoryData)serializer.Deserialize(stream);
                // Restore data to PlayerInfo
                PlayerInventory = data.PlayerInventory;
                TreasureChest1 = data.TreasureChest1;
                TreasureChest2 = data.TreasureChest2;
                TreasureChest3 = data.TreasureChest3;
                TreasureChest4 = data.TreasureChest4;
            }
        }
        else
        {
            Debug.LogWarning("Save file not found at: " + path);
        }
    }
}
