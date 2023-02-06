using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New RecipeInventory", menuName = "Inventory System/RecipeInventory")]
public class RecipeInventory : InventoryObject
{
    //数据库
    public RecipeDatabaseObject recipeDatabase;
    //理想应该是当前背包里的所有东西是否可以搞一个菜出来
    // public void Awake()
    // {
    //     this.Container.Items = new InventorySlot[5];
    // }

    public ItemObject TryMakeDishes()
    {
        List<int> mateiralID = GetPlayerMaterialIDList(Container);
        for (int i = 0; i < recipeDatabase.GetRecipe.Count; i++)
        {
            RecipeObject recipe = recipeDatabase.GetRecipe[i];
            //数量不对直接跳过
            Debug.Log(recipe.material.Length);
            if (recipe.material.Length != mateiralID.Count) continue;
            List<int> recipeIDs = new List<int>();
            for (int j = 0; j < recipe.material.Length; j++)
            {
                Debug.Log("recipeMaterial ID " + recipe.material[j].Id);
                recipeIDs.Add(recipe.material[j].Id);
            }
            for (int j = 0; j < mateiralID.Count; j++)
            {
                if (!recipeIDs.Contains(mateiralID[j])) return null;
            }
            return recipe.result;
        }
        return null;
    }

    public List<int> GetPlayerMaterialIDList(Inventory inventory)
    {
        List<int> itemIDs = new List<int>();
        for (int i = 0; i < inventory.Items.Length; i++)
        {
            if (inventory.Items[i].ID == -1) continue;
            Debug.Log("material ID " + inventory.Items[i].ID);
            itemIDs.Add(inventory.Items[i].ID);
        }
        return itemIDs;
    }

}
