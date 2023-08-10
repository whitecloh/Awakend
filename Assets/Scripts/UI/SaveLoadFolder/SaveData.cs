using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public PlayerData MyPlayerData { get; set; }

    public List<LootData> MyLootData { get; set; }

    public InventoryData MyInventoryData { get; set; }

    public List<EquipmentData> MyEquipmentData { get; set; }

    public List<QuestData> MyQuestData { get; set; }

    public List<QuestGiverData> MyQuestGiverData { get; set; }

    public DateTime MyDateTime { get; set; }

    public string MyScene { get; set; }

    public SaveData()
    {
        MyInventoryData = new InventoryData();
        MyLootData = new List<LootData>();
        MyEquipmentData = new List<EquipmentData>();
        MyQuestData = new List<QuestData>();
        MyQuestGiverData = new List<QuestGiverData>();
        MyDateTime = DateTime.Now;
    }
}
[Serializable]
public class PlayerData
{
    public int MyLevel { get; set; }
    public float MyXP { get; set; }
    public float MyMaxXP { get; set; }
    public float MyHealth { get; set; }
    public float MyMaxHealth { get; set; }
    public float MyMana { get; set; }
    public float MyMaxMana { get; set; }
    public float MyX { get; set; }
    public float MyY { get; set; }
    public float MyZ { get; set; }
    public PlayerData(int level,float xp,float maxXp,float health,float maxHealth,float mana,float maxMana, Vector3 position)
    {
        this.MyLevel = level;
        this.MyXP = xp;
        this.MyMaxXP = maxXp;
        this.MyHealth = health;
        this.MyMaxHealth = maxHealth;
        this.MyMana = mana;
        this.MyMaxMana = maxMana;
        this.MyX = position.x;
        this.MyY = position.y;
        this.MyZ = position.z;
    }
}
[Serializable]
public class ItemData
{
    public string MyTitle { get; set; }
    public int MyStackCount { get; set; }
    public int MySlotIndex { get; set; }    

    public ItemData(string title,int stackCount=0,int slotIndex=0)
    {
        MyTitle = title;
        MyStackCount = stackCount;
        MySlotIndex = slotIndex;
    }
}

[Serializable]
public class LootData
{
    public string MyName { get; set;}
    public List<ItemData> MyItems { get; set;}

    public LootData(string name)
    {
        MyName = name;
        MyItems = new List<ItemData>();
    }
}

[Serializable]
public class BagData
{
    public int MySlotCount { get; set; }
    public int MyBagIndex { get; set; }

    public BagData(int count,int index)
    {
        MySlotCount = count;
        MyBagIndex = index;
    }
}

[Serializable]
public class InventoryData
{
    public List<BagData> MyBags { get; set; }

    public List<ItemData> MyItems { get; set; }

    public InventoryData()
    {
        MyBags = new List<BagData>();
        MyItems = new List<ItemData>();
    }
}

[Serializable]
public class EquipmentData
{
    public string MyTitle { get; set; }
    public string MyType { get; set; }

    public EquipmentData (string title,string type)
    {
        MyTitle = title;
        MyType = type;
    }
}

[Serializable]
public class QuestData
{
    public string MyTitle { get; set; }
    public string MyDescription { get; set; }

    public CollectObjective[] MyCollecObjectives { get; set; }
    public KillObjective[] MyKillObjectives { get; set; }

    public int MyQuestGiverID { get; set; }

    public QuestData(string title, string description, CollectObjective[] collectObjectives, KillObjective[] killObjectives, int questGiverId)
    {
        MyTitle = title;
        MyDescription = description;
        MyCollecObjectives = collectObjectives;
        MyKillObjectives = killObjectives;
        MyQuestGiverID = questGiverId;
    }
}

[Serializable]
public class QuestGiverData
{
    public List<string> MyCompleteQuests { get; set; }

    public int MyQuestGiverID { get; set; }

    public QuestGiverData(List<string> completeQuests,int giverID)
    {
        MyCompleteQuests = completeQuests;
        MyQuestGiverID = giverID;
    }
}














/*[Serializable]
public class ActionButtonData
{
    public string MyAction { get; set; }
    public bool IsItem { get; set; }
    public int MyIndex { get; set; }

    public ActionButtonData(string action , bool isItem, int index)
    {
        MyAction = action;
        IsItem = isItem;
        MyIndex = index;
    }
}*/

