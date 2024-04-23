using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorUp : MonoBehaviour
{
    public GameObject Floor,fall,up;

    public float upspeed,Low_y,High_y;
    public GameObject player;
    public bool stay=false,ener=false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        floorUp();
    }
    public void floorUp(){
        if(Floor.transform.position.y<=Low_y||Floor.transform.position.y<High_y){    
            Floor.transform.Translate(0,upspeed*Time.deltaTime,0);
            if(stay==true){
                if(ener==true){
                    player.transform.Translate(0,upspeed*Time.deltaTime,0);
                }
            }
        }
        if(Floor.transform.position.y>=High_y){
            fall.SetActive(true);
            up.SetActive(false);
            ener=false;
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
