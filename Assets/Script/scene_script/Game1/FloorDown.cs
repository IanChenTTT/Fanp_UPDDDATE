using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDown : MonoBehaviour
{
    // Start is calle
    public GameObject Floor,fall,up;
    public float fallspeed,Low_y,High_y;
    public GameObject player;
    public bool stay,ener;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        floorfall();
    }
    public void floorfall(){
        if(Floor.transform.position.y<=High_y&&Floor.transform.position.y>Low_y||Floor.transform.position.y>High_y){    
            Floor.transform.Translate(0,fallspeed*Time.deltaTime,0);
            if(stay==true){
                if(ener==true){
                    player.transform.Translate(0,fallspeed*Time.deltaTime,0);
                }
            }
        }
        if(Floor.transform.position.y<=Low_y){
            fall.SetActive(false);
            up.SetActive(true);
        }
    }
        void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player"){
            ener=true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag=="Player"){
            stay=false;
            ener=false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag=="Player"){
            stay=true;
        }
    }
}
