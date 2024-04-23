using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_place : MonoBehaviour
{
    public GameObject item;
    // Start is called before the first frame update
    void Start()
    {
        print(item.transform.position.x);
    }

    // Update is called once per frame
    void Update()
    {
         print(item.transform.position.x);
    }
}
