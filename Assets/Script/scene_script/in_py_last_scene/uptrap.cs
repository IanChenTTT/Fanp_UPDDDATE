using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class uptrap : MonoBehaviour
{
    public float y,timer1,timer2,timer3,wait;
    //timer2比較快
    public GameObject trap;
    public GameObject Up,Down;
    public bool first=false,second=false;
    // Start is called before the first frame update
    void Start()
    {
        first=true;
    }

    // Update is called once per frame
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        Start();
    }
    void Update()
    {
        if(first==true){
            first_up();
        }
        else if(second==true){
            All_up();
        }
    }



    public void first_up(){
        trap.transform.Translate(0,y*Time.deltaTime,0);
        Invoke("first_turn_false",timer1);
    }

    public void All_up(){
        trap.transform.Translate(0,y*Time.deltaTime,0);
        Invoke("second_turn_false",timer2);
        
    }
    public void down_open(){
        Up.SetActive(false);
        Down.SetActive(true);
    }
    public void first_turn_false(){
        first=false;
        Invoke("secondtrue",wait);
        
    }
    public void second_turn_false(){
        second=false;
        Invoke("down_open",timer3);
    }
    public void secondtrue(){
        second=true;
    }
}
