using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLeft : MonoBehaviour
{ 
    public GameObject Floor,Left,Right;
    public float Leftspeed,Left_x,Right_x;
    public GameObject player;
    public bool player_enter=false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        floorleft();
    }
    public void floorleft(){
        if(Floor.transform.position.x<=Right_x&&Floor.transform.position.x>Left_x||Floor.transform.position.x>Right_x){    
            Floor.transform.Translate(Leftspeed*Time.deltaTime,0,0);
            if(FloorRight.player_stay==true){
                if(player_enter==true){
                    player.transform.Translate(Leftspeed*Time.deltaTime,0,0);
                }
            }
        }
        if(Floor.transform.position.x<=Left_x){
            Left.SetActive(false);
            Right.SetActive(true);
            player_enter=false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player"){
            player_enter=true;
        }
    }
        void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag=="Player"){
            FloorRight.player_stay=true;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag=="Player"){
            FloorRight.player_stay=false;
            player_enter=false;
        }
    }
}
