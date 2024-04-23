using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class behind_py_sc3_trapmove : MonoBehaviour
{
    public GameObject trapT,trapL,trapR;


    public float trapl,trapr,trapt;
    public bool trapmoving=false;
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
    
    public void trap_fall(){
        trapT.transform.Translate(0,-trapt,0);
        trapR.transform.Translate(-trapr,0,0);
        trapL.transform.Translate(trapl,0,0);
    }
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
            trapmoving=true;
            print("1");
    }
    public void booltrue(){
        if(trapmoving==true){
            Invoke("trap_fall",0.1f);
        }
    }
    public void stop(){
        if(trapR.transform.position.x<=546){
            trapR.SetActive(false);
        }
        if(trapL.transform.position.x>=598){
            trapL.SetActive(false);
        }
        if(trapT.transform.position.y==-24){
            trapT.SetActive(false);
        }
    }
}
