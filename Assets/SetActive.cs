using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetActive : MonoBehaviour
{
    
    public bool ishide;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard.hKey.wasPressedThisFrame) {
            Debug.Log("prepare to close the bag");
            Debug.Log(ishide);
            this.gameObject.transform.GetChild(0).gameObject.SetActive(ishide);
            ishide = !ishide;
        }
    }
}
