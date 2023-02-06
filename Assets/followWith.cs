using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followWith : MonoBehaviour
{
    [SerializeField]public Transform LookAt;
    [SerializeField]public Vector3 offset;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(LookAt.position + offset);

        if (transform.position != pos)
            transform.position = pos;
    }
}
