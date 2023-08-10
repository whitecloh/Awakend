using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private Item[] _items;
    [SerializeField]
    private SavedGame[] _saveSlots;

    private LootTable[] _loot;
    private CharButton[] _equipment;

    private string _action;

    private void Awake()
    {
        _loot = FindObjectsOfType<LootTable>();
        _equipment = FindObjectsOfType<CharButton>();

        foreach(SavedGame saved in _saveSlots)
        {
            ShowSavedFiles(saved);
        }

    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "SampleScene") return;

        if (PlayerPrefs.HasKey("Load"))
        {
            Player.MyInstance.GetAgent.enabled = false;
            Load(_saveSlots[PlayerPrefs.GetInt("Load")]);
            PlayerPrefs.DeleteKey("Load");
        }
        else
        {
            MessageFeedManager.Instance.WriteMessage("Очисти территорию от нечести");
            MessageFeedManager.Instance.WriteMessage("Город атакован!");
            Player.MyInstance.SetDefaultsValues();
        }
    }

    private void ShowSavedFiles(SavedGame saved)
    {
        if (File.Exists(Application.persistentDataPath + "/" + saved.gameObject.name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + saved.gameObject.name + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            saved.ShowInfo(data);
        }
        else
        {
            saved.HideVisuals();
        }
    }
    public void SaveLoadButtons(GameObject gameObject)
    {
        _action = gameObject.name;

        switch (_action)
        {
            case "LoadButton":
                LoadScene(gameObject.GetComponentInParent<SavedGame>());
                break;
            case "SaveButton":
                Save(gameObject.GetComponentInParent<SavedGame>());
                break;
            case "DeleteButton":
                Delete(gameObject.GetComponentInParent<SavedGame>());
                break;
        }
    }

    private void LoadScene(SavedGame saved)
    {
         if (File.Exists(Application.persistentDataPath + "/" + saved.gameObject.name + ".dat"))
         {
             BinaryFormatter bf = new BinaryFormatter();
             FileStream file = File.Open(Application.persistentDataPath + "/" + saved.gameObject.name + ".dat", FileMode.Open);
             SaveData data = (SaveData)bf.Deserialize(file);
             file.Close();

             PlayerPrefs.SetInt("Load", saved.Index);
            SceneManager.LoadScene(data.MyScene);
        }
    }


    private void Save(SavedGame saved)
    {
        if (SceneManager.GetActiveScene().name != "SampleScene") return;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + saved.gameObject.name + ".dat", FileMode.Create);
            
            SaveData data = new SaveData();

            data.MyScene = SceneManager.GetActiveScene().name;
            SavePlayer(data);
            //SaveChests(data);
            SaveEquipment(data);
            SaveQuests(data);
            SaveQuestGiver(data);
            SaveInventory(data);
            SaveBags(data);
            bf.Serialize(file, data);
            file.Close();

            ShowSavedFiles(saved);
        }
        catch(System.Exception)
        {
            Delete(saved);
            PlayerPrefs.DeleteKey("Load");
        }
    }

    private void Load(SavedGame saved)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + saved.gameObject.name + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            LoadPlayer(data);
            //LoadChests(data);
            LoadEquipment(data);
            LoadQuestGiver(data);
            LoadQuests(data);
            LoadInventory(data);
            LoadBags(data);
        }
        catch (System.Exception)
        {
            //Delete(saved);
           // Player.MyInstance.SetDefaultsValues();
           //PlayerPrefs.DeleteKey("Load");
           //SceneManager.LoadScene("SampleScene");
        }
    }

    private void Delete(SavedGame saved)
    {
        File.Delete(Application.persistentDataPath + "/" + saved.gameObject.name + ".dat");
        saved.HideVisuals();
    }



    private void SavePlayer(SaveData data)
    {
            data.MyPlayerData = new PlayerData(Player.MyInstance.MyLevel,
            Player.MyInstance.MyXp.MyCurrentValue,Player.MyInstance.MyXp.MyMaxValue,
            Player.MyInstance.MyHealth.MyCurrentValue,Player.MyInstance.MyHealth.MyMaxValue,
            Player.MyInstance.MyMana.MyCurrentValue,Player.MyInstance.MyMana.MyMaxValue,
            Player.MyInstance.transform.position);
    }
    private void LoadPlayer(SaveData data)
    {     
        Player.MyInstance.MyLevel = data.MyPlayerData.MyLevel;
        Player.MyInstance.UpdateLevel();
        Player.MyInstance.MyHealth.Initialize(data.MyPlayerData.MyHealth, data.MyPlayerData.MyMaxHealth);
        Player.MyInstance.MyXp.Initialize(data.MyPlayerData.MyXP, data.MyPlayerData.MyMaxXP);
        Player.MyInstance.MyMana.Initialize(data.MyPlayerData.MyMana, data.MyPlayerData.MyMaxMana);
        Player.MyInstance.transform.position = new Vector3(data.MyPlayerData.MyX, data.MyPlayerData.MyY, data.MyPlayerData.MyZ);
    }

    private void SaveChests(SaveData data)
    {
        for(int i=0;i<_loot.Length;i++)
        {
            data.MyLootData.Add(new LootData(_loot[i].name));

            foreach(Item item in _loot[i]._droppedItems)
            {
                if(_loot[i]._droppedItems.Count>0)
                {
                    data.MyLootData[i].MyItems.Add(new ItemData(item.Title, item.MySlot.MyItems.Count,item.MySlot.MyIndex));
                }
            }
        }
    }
    private void LoadChests(SaveData data)
    {
        Debug.Log("Chests");
        foreach (LootData loot in data.MyLootData)
        {
            LootTable lt = Array.Find(_loot, x => x.name == loot.MyName);

            foreach(ItemData itemData in loot.MyItems)
            {
                Item item =Instantiate(Array.Find(_items, x => x.Title == itemData.MyTitle));
                lt._droppedItems.Add(item);
            }
        }
    }

    private void SaveBags(SaveData data)
    {
            data.MyInventoryData.MyBags.Add(new BagData(InventoryScript.Instance.MyBags[0].Slots,0));
    }
    private void LoadBags(SaveData data)
    {
        foreach (BagData bagData in data.MyInventoryData.MyBags)
        {
            Bag newBag = (Bag)Instantiate(_items[0]);
            newBag.Initialize(bagData.MySlotCount);
            InventoryScript.Instance.AddBag(newBag, bagData.MyBagIndex);
        }
    }

    private void SaveEquipment(SaveData data)
    {
        foreach(CharButton charButton in _equipment)
        {
            if(charButton.EquippedArmor!=null)
            {
                data.MyEquipmentData.Add(new EquipmentData(charButton.EquippedArmor.Title, charButton.name));
            }
        }
    }
    private void LoadEquipment(SaveData data)
    {
        foreach(EquipmentData equip in data.MyEquipmentData)
        {
            CharButton cb = Array.Find(_equipment, x => x.name == equip.MyType);
            cb.EquipArmor(Array.Find(_items,x=>x.Title == equip.MyTitle)as Armor);
        }
    }

    private void SaveInventory(SaveData data)
    {
        List<SlotScript> slots = InventoryScript.Instance.GetAllItems();

        foreach(SlotScript slot in slots)
        {
            data.MyInventoryData.MyItems.Add(new ItemData(slot.MyItem.Title,slot.MyItems.Count,slot.MyIndex));
        }
    }
    private void LoadInventory(SaveData data)
    {
        foreach (ItemData itemData in data.MyInventoryData.MyItems)
        {
            Item item = Instantiate(Array.Find(_items, x => x.Title == itemData.MyTitle));
            for (int i=0;i<itemData.MyStackCount;i++)
            {
                InventoryScript.Instance.PlaceInSpecific(item, itemData.MySlotIndex);
            }
        }
    }

    private void SaveQuests(SaveData data)
    {
        foreach(Quest quest in QuestLog.Instance.Quests)
        {
            data.MyQuestData.Add(new QuestData(quest.Title,quest.Description,quest.CollectObjectives,quest.KillObjectives,quest.MyQuestGiver.QuestGiverId));
        }
    }
    private void LoadQuests(SaveData data)
    {
        QuestGiver[] questGivers = FindObjectsOfType<QuestGiver>();

        foreach( QuestData questData in data.MyQuestData)
        {
            QuestGiver qg = Array.Find(questGivers, x => x.QuestGiverId == questData.MyQuestGiverID);
            Quest q = Array.Find(qg.Quests, x => x.Title == questData.MyTitle);
            q.MyQuestGiver = qg;
            q.KillObjectives = questData.MyKillObjectives;
            QuestLog.Instance.AcceptQuest(q);
        }
    }

    private void SaveQuestGiver(SaveData data)
    {
        QuestGiver[] questGivers = FindObjectsOfType<QuestGiver>();

        foreach(QuestGiver questGiver in questGivers)
        {
            data.MyQuestGiverData.Add(new QuestGiverData(questGiver.CompletedQuests, questGiver.QuestGiverId));
        }
    }

    private void LoadQuestGiver(SaveData data)
    {
        QuestGiver[] questGivers = FindObjectsOfType<QuestGiver>();
        foreach(QuestGiverData questGiverData in data.MyQuestGiverData)
        {
            QuestGiver questGiver = Array.Find(questGivers, x => x.QuestGiverId == questGiverData.MyQuestGiverID);
            questGiver.CompletedQuests = questGiverData.MyCompleteQuests;
            questGiver.UpdateQuestStatus(); 
        }
    }
}
