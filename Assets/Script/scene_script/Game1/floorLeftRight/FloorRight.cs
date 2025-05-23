using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorRight : MonoBehaviour
{
    public GameObject Floor,Left,Right;
    public GameObject player;
    public float Rightspeed,Left_x,Right_x;
    public static bool player_stay=false;
    public bool player_enter=false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        floorRight();
    }
    public void floorRight(){
        if(Floor.transform.position.x<=Right_x||Floor.transform.position.x<Right_x){    
            Floor.transform.Translate(Rightspeed*Time.deltaTime,0,0);
            if(player_stay==true){
                if(player_enter==true){
                    player.transform.Translate(Rightspeed*Time.deltaTime,0,0);
                }
                
            }
        }
        if(Floor.transform.position.x>=Right_x){
            Left.SetActive(true);
            Right.SetActive(false);
            player_enter=false;
        }
    }
  
  /// <summary>
  /// Sent when another object enters a trigger collider attached to this
  /// object (2D physics only).
  /// </summary>
  /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player"){
            player_enter=true;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag=="Player"){
            player_stay=true;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag=="Player"){
            player_stay=false;
            player_enter=false;
        }
    }
}
