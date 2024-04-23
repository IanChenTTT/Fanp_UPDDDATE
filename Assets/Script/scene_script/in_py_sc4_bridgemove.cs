using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class in_py_sc4_bridgemove : MonoBehaviour
{
    public GameObject movebridge;
    public GameObject movecollider;
    public float Speed=5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player"){
            movebridge.transform.Translate(10.14f,0,0);
            movecollider.SetActive(false);
        }
    }
}
