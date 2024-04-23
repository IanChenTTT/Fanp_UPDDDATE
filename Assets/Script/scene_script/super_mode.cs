using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class super_mode : MonoBehaviour
{
    public GameObject coll;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void super(){
      if(Input.GetKeyDown(KeyCode.X)){
        coll.SetActive(false);
        }
    }
}
