using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe Object", menuName = "Inventory System/Items/Recipe")]

public class RecipeObject : ScriptableObject
{
    // Start is called before the first frame update
    public int Id;
    //原料集合，设置不超过5种吧
    public ItemObject[] material;
    //结果集合
    public ItemObject result;
}
