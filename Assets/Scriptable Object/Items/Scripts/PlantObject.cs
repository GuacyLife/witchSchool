using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Plant Object", menuName = "Inventory System/Items/Plant")]
public class PlantObject : ScriptableObject
{
    public int Id;
    //阶段
    public string Name;
    public Sprite[] sprites;

    public ItemObject product;

    public Plant createPlant()
    {
        Plant plant = new Plant(this);
        return plant;
    }
}

[System.Serializable]
public class Plant
{
    public int Id;
    public string name;

    public Plant(PlantObject plantObject)
    {
        Id = plantObject.Id;
        name = plantObject.Name;
    }

    public Plant()
    {
        name = "";
        Id = -1;
    }
}