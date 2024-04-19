using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class downtrap : MonoBehaviour
{
    public float y,timer1,timer2;
    public GameObject trap;
    public GameObject Up,Down;
    public bool downBool=false;
    // Start is called before the first frame update
    void Start()
    {
        downBool=true;
    }

    void OnEnable()
    {
        Start();
    }
    // Update is called once per frame
    void Update()
    {
        if(downBool==true){
            down();
        }
    }

    public void down(){
        trap.transform.Translate(0,-y*Time.deltaTime,0);
        Invoke("booloff",timer1);
    }
    public void up_open(){
        Up.SetActive(true);
        Down.SetActive(false);
    }
    public void booloff(){
        downBool=false;
        Invoke("up_open",timer2);
    }
}
