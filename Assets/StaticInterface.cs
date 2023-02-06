using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInterface : UserInterface
{
    public GameObject[] slots;
    public RecipeInventory recipeInventory;


    public void MakeDishes()
    {
        ItemObject item = recipeInventory.TryMakeDishes();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            itemsDisplayed[objList[i]].item = new Item();
        }
        //显示一下做好的东西
        inventory.Container.Clear();
        if (item != null)
        {
            Debug.Log(item.name);
            inventory.AddItem(item.CreateItem(), 1);
        }

    }

    public override void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        objList = new List<GameObject>();
        this.inventory = recipeInventory;
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = slots[i];

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
            objList.Add(obj);
        }
    }
}
