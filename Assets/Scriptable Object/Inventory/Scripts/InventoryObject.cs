using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]

public class InventoryObject : ScriptableObject
{
    public string savePath;

    public ItemDatabaseObject database;

    public Inventory Container;

    public string GetEnumType(ItemObject item)
    {
        if (database.GetItem[item.Id])
        {
            int type = (int)database.GetItem[item.Id].type;
            if (type == 0) return "Food";
            else if (type == 2) return "Default";
            else if (type == 3) return "Tool";
        }
        return "";
    }

    //     private void OnEnable()
    //     {
    // #if UNITY_EDITOR
    //         database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/TestDatabase.asset", typeof(ItemDatabaseObject));        
    // #else
    //         database = Resources.Load<ItemDatabaseObject>("TestDatabase");
    // #endif

    //     }
    public void AddItem(Item _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID == _item.Id)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }
        SetEmptySlot(_item, _amount);

    }

    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID <= -1)
            {
                Container.Items[i].UpdateSlot(_item.Id, _item, _amount);
                return Container.Items[i];
            }
        }
        return null;
    }

    public void MoveItem(InventorySlot Item1, InventorySlot Item2)
    {
        InventorySlot temp = new InventorySlot(Item2.ID, Item2.item, Item2.amount);
        Item2.UpdateSlot(Item1.ID, Item1.item, Item1.amount);
        Item1.UpdateSlot(temp.ID, temp.item, temp.amount);
    }

    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item == _item)
            {
                Container.Items[i].UpdateSlot(-1, null, 0);
            }
        }
    }

    [ContextMenu("Save")]
    public void Save()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        Debug.Log(string.Concat(Application.persistentDataPath, savePath));
        formatter.Serialize(stream, Container);
        stream.Close();
        // The player can change the game data easily
        // string saveData = JsonUtility.ToJson(this, true);
        // BinaryFormatter bf = new BinaryFormatter();
        // FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        // bf.Serialize(file, saveData);
        // file.Close();

    }
    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            // //The way player can change our game data easily
            // BinaryFormatter bf = new BinaryFormatter();
            // FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            // JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            // file.Close();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < Container.Items.Length; i++)
            {
                InventorySlot newSlot = newContainer.Items[i];
                Container.Items[i].UpdateSlot(newSlot.ID, newSlot.item, newSlot.amount);
            }
            stream.Close();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }
}
[System.Serializable]
public class Inventory
{
    //规定初始化背包有多少个格子
    public InventorySlot[] Items = new InventorySlot[15];
    public void Clear()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].UpdateSlot(-1, new Item(), 0);
        }
    }
}
[System.Serializable]
public class InventorySlot
{

    public ItemType[] AllowedItems = new ItemType[0];
    [System.NonSerialized]
    public UserInterface parent;
    public int ID = -1;
    public Item item;
    public int amount;
    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
    }
    public InventorySlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void UpdateSlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
    public bool CanPlaceInSlot(ItemObject _item)
    {
        if (AllowedItems.Length <= 0)
            return true;
        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (_item.type == AllowedItems[i])
                return true;
        }
        return false;
    }
}