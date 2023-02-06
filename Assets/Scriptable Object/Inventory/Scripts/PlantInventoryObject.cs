using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Plant Inventory", menuName = "Inventory System/PlantInventory")]

public class PlantInventoryObject : ScriptableObject
{
    public string savePath;

    public ItemDatabaseObject database;

    public PlantDatabaseObject plantDatabase;

    public PlantInventory Container;


    //     private void OnEnable()
    //     {
    // #if UNITY_EDITOR
    //         database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/TestDatabase.asset", typeof(ItemDatabaseObject));        
    // #else
    //         database = Resources.Load<ItemDatabaseObject>("TestDatabase");
    // #endif

    //     }
    
    //种植
    public void AddPlant(Plant _plant, int _index)
    {
        if (_index < 8 && Container.Plants[_index].ID == -1)
        {
            Container.Plants[_index].UpdateSlot(_plant.Id, _plant, 0);
            Debug.Log("add plant");
        }
    }
    //浇水/成熟
    public void AddPeried(int _index)
    {
        Container.Plants[_index].AddPeried();
    }
    //收获
    public Item GetProduct(int _index)
    {
        if (_index < 8 && Container.Plants[_index].peried == 1)
        {
            Item product = plantDatabase.GetPlant[Container.Plants[_index].ID].product.CreateItem();
            Container.Plants[_index].UpdateSlot(-1, null, 0);
            return product;
        }
        return null;
    }

    // public void RemoveItem(Item _item)
    // {
    //     for (int i = 0; i < Container.Items.Length; i++)
    //     {
    //         if (Container.Items[i].item == _item)
    //         {
    //             Container.Items[i].UpdateSlot(-1, null, 0);
    //         }
    //     }
    // }

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
            PlantInventory newContainer = (PlantInventory)formatter.Deserialize(stream);
            for (int i = 0; i < Container.Plants.Length; i++)
            {
                PlantSlot newSlot = newContainer.Plants[i];
                Container.Plants[i].UpdateSlot(newSlot.ID, newSlot.plant, newSlot.peried);
            }
            stream.Close();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new PlantInventory();
    }
}
[System.Serializable]
public class PlantInventory
{
    //规定初始化背包有多少个格子
    public PlantSlot[] Plants = new PlantSlot[8];
    public void Clear()
    {
        for (int i = 0; i < Plants.Length; i++)
        {
            Plants[i].UpdateSlot(-1, new Plant(), 0);
        }
    }
}
[System.Serializable]
public class PlantSlot
{

    // [System.NonSerialized]
    // public UserInterface parent;
    public int ID = -1;
    public Plant plant;
    // 植物生长的周期数
    public int peried;
    public PlantSlot()
    {
        ID = -1;
        plant = null;
        //这个peried的问题之后再想
        peried = 0;
    }
    public PlantSlot(int _id, Plant _plant, int _peried)
    {
        ID = _id;
        plant = _plant;
        peried = _peried;
    }
    public void UpdateSlot(int _id, Plant _plant, int _peried)
    {
        ID = _id;
        plant = _plant;
        peried = _peried;
    }
    public void AddPeried()
    {
        if (peried < 1)
            peried += 1;
    }
}