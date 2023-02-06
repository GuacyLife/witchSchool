using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe Database", menuName = "Inventory System/Recipes/Database")]
public class RecipeDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public RecipeObject[] Recipes;
        public Dictionary<int, RecipeObject> GetRecipe = new Dictionary<int, RecipeObject>();


    public void OnBeforeSerialize(){
        GetRecipe = new Dictionary<int, RecipeObject>();
    }

    public void OnAfterDeserialize(){
        for (int i = 0; i < Recipes.Length; i++)
        {
            Recipes[i].Id = i;
            Debug.Log(i);
            GetRecipe.Add(i, Recipes[i]);
        }
    }
    
}
