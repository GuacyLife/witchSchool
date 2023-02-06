using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant Database", menuName = "Inventory System/Items/PlantDatabase")]
public class PlantDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public PlantObject[] plants;
    public Dictionary<int, PlantObject> GetPlant = new Dictionary<int, PlantObject>();


    public void OnBeforeSerialize(){
        GetPlant = new Dictionary<int, PlantObject>();
    }

    public void OnAfterDeserialize(){
        for (int i = 0; i < plants.Length; i++)
        {
            
            plants[i].Id = i;
            
            GetPlant.Add(i, plants[i]);
            //Debug.Log(Items[i].name + " " + Items[i].Id);
        }
    }
    
}
