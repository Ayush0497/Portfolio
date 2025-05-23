using System.Xml.Serialization;
[System.Serializable]
[XmlInclude(typeof(WeaponType))] // Include derived types
[XmlInclude(typeof(ArmorType))]
[XmlInclude(typeof(ToolType))]
public class Item
    {
        public string Name;
        public int ID;
    }

    [System.Serializable]
    public abstract class Weapon : Item
    {
        public int Damage;
    }

    [System.Serializable]
    public abstract class Armor : Item
    {
        public int Defense;
    }

    [System.Serializable]
    public abstract class Tool : Item
    {
        public int Maxhealth;
    }
