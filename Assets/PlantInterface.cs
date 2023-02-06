using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class PlantInterface : MonoBehaviour
{
    public List<GameObject> objList = new List<GameObject>();
    public GameObject[] slots;
    public PlantInventoryObject PlantInventory;

    public Dictionary<GameObject, PlantSlot> PlantsDisplayed = new Dictionary<GameObject, PlantSlot>();
    // Start is called before the first frame update
    void Start()
    {
        createSlots();
    }

    public void createSlots()
    {
        PlantsDisplayed = new Dictionary<GameObject, PlantSlot>();
        objList = new List<GameObject>();
        for (int i = 0; i < PlantInventory.Container.Plants.Length; i++)
        {
            var obj = slots[i];

            PlantsDisplayed.Add(obj, PlantInventory.Container.Plants[i]);
            objList.Add(obj);
        }
    }

    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, PlantSlot> _slot in PlantsDisplayed)
        {
            if (_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().sprite = PlantInventory.plantDatabase.GetPlant[_slot.Value.plant.Id].sprites[_slot.Value.peried];
                //设置颜色
                _slot.Key.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
                _slot.Key.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().drawMode = (SpriteDrawMode)1;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().size = new Vector2(0.4f,0.4f);
                
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().sprite = null;
                //设置颜色
                _slot.Key.transform.GetChild(0).GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
    }
}
