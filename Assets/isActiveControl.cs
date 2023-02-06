using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isActiveControl : MonoBehaviour
{
    // Start is called before the first frame update
    // GetChild(0) 是UI plant
    // GetChild(1) 是UI water
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void SetChildActive(bool isActive)
    {
        transform.GetChild(0).gameObject.SetActive(isActive);
        transform.GetChild(1).gameObject.SetActive(isActive);    
        transform.GetChild(2).gameObject.SetActive(isActive);    
    }
}
