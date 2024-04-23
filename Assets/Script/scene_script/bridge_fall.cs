using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridge_fall : MonoBehaviour
{
    public GameObject bridge;
    public GameObject Bridge_fall_control;
    public bool fall=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        booltrue();
        stop();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        fall=true;
    }
    public void Bridge_Fall(){
        bridge.transform.Translate(0,-0.04f,0);
    }
    public void booltrue(){
        if(fall==true){
            Invoke("Bridge_Fall",0.1f);
        }
    }
    public void stop(){
        if(bridge.transform.position.y<=-8){
            bridge.SetActive(false);
            Bridge_fall_control.SetActive(false);
        }
    }
}
