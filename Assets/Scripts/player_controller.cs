using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player_controller : MonoBehaviour
{

    public MouseItem mouseItem = new MouseItem();
    public InventoryObject MaterialInventory;

    public InventoryObject FoodInventory;

    public InventoryObject ToolInventory;

    //感觉似乎这样耦合的太紧密了。。。farm的inventory理论上来说不应该是player负责的 回头改改
    public PlantInventoryObject plantInventory;

    public Dictionary<string, InventoryObject> InventoryEnum = new Dictionary<string, InventoryObject>();

    //public int nowIndex = 0;

    //public InventoryObject[] inventories;

    public float moveSpeed = 1f;

    public Keyboard keyboard = Keyboard.current;

    public ContactFilter2D movementFilter;

    public float collisonOffset = 0.05f;
    Vector2 movementInput;
    //用来捕捉人物上下左右移动的二维变量
    Rigidbody2D rb;

    Animator animator;
    //碰撞物体
    List<RaycastHit2D> castCollosions = new List<RaycastHit2D>();
    //种田相关的设置
    public GameObject FarmObj;

    // Start is called before the first frame update
    void Start()
    {
        InventoryEnum.Add("Food", FoodInventory);
        InventoryEnum.Add("Default", MaterialInventory);
        InventoryEnum.Add("Tool", ToolInventory);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            TryMove(movementInput);
        }
        else
        {
            animator.SetBool("isMoving", false);
            //静止动画
        }

        if (keyboard.spaceKey.isPressed)
        {
            Debug.Log("Space is released and Inventory is saved");
            MaterialInventory.Save();
            FoodInventory.Save();
            ToolInventory.Save();
        }

        if (keyboard.enterKey.isPressed)
        {
            Debug.Log("Enter is released and Inventory is loaded");
            MaterialInventory.Load();
            FoodInventory.Load();
            ToolInventory.Load();
        }
    }
    // 确认是否有障碍物
    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
            movementInput,
            movementFilter,
            castCollosions,
            moveSpeed * Time.fixedDeltaTime * collisonOffset
        );

        if (count == 0)
        {
            rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
            animator.SetFloat("aniMoveX", direction.x);
            animator.SetFloat("aniMoveY", direction.y);
            animator.SetBool("isMoving", true);
            return true;
        }
        else
        {
            animator.SetBool("isMoving", false);
            return false;
        }
    }
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<GroundItem>();
        //Debug.Log("enter this");
        if (item)
        {
            if (!MaterialInventory.GetEnumType(item.item).Equals(""))
            {
                //Debug.Log(MaterialInventory.GetEnumType(item.item));
                InventoryEnum[MaterialInventory.GetEnumType(item.item)].AddItem(new Item(item.item), 1);
                Destroy(other.gameObject);
            }
        }


        //如果碰到农田
        if (other.tag == "FarmSlotParent")
        {
            GameObject.Find("Canvas-DoPlantUI").SendMessage("SetChildActive", true);
        }

        if (other.tag == "FarmSlot")
        {
            FarmObj = other.gameObject;
            Debug.Log(other.gameObject.name);
        }
    }


    public void OnTriggerExit2D(Collider2D other)
    {
        //如果离开农田
        if (other.tag == "FarmSlotParent")
        {
            GameObject.Find("Canvas-DoPlantUI").SendMessage("SetChildActive", false);
            FarmObj = null;
        }

    }

    public void ClearInventory()
    {
        MaterialInventory.Container.Items = new InventorySlot[15];
        FoodInventory.Container.Items = new InventorySlot[15];
    }

    //看情况 之后应该是选择一个index
    public void DoPlant(PlantObject _plantObject)
    {
        Plant plt = _plantObject.createPlant();
        //种地
        if (FarmObj)
        {
            plantInventory.AddPlant(plt, int.Parse(FarmObj.name));
        }
    }

    //浇水，暂时设计成浇一次水就结果
    public void DoWater()
    {
        if (FarmObj)
        {
            plantInventory.AddPeried(int.Parse(FarmObj.name));
        }
    }

    public void DoGetProduct()
    {
        if (FarmObj)
        {
            Item product = plantInventory.GetProduct(int.Parse(FarmObj.name));
            MaterialInventory.AddItem(product, 1);
            Debug.Log("收获了一个西红柿！");
        }

    }


    public void OnApplicationQuit()
    {
        //inventory.Container.Items = new InventorySlot[15];
        ClearInventory();
    }
}

[System.Serializable]
public class Player
{
    //人物的五维，分别是熵、技巧、感受、体力、智慧
    public int entropy;
    public int skill;
    public int feel;
    public int physicalStrength;
    public int wisdom;
    //有多少钱
    public int money;
    //叫什么名字
    public string name;

    public void InitialPlayerWithNumber(int _entropy, int _skill, int _feel, int _physicalStrength, int _wisdom)
    {
        //游戏初始化
        entropy = _entropy;
        skill = _skill;
        feel = _feel;
        physicalStrength = _physicalStrength;
        wisdom = _wisdom;
        money = 0;
    }

    public void InitialPlayerWithName(string _name)
    {
        name = _name;
    }
}