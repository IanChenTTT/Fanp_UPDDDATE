using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UP : MonoBehaviour
{
    public GameObject Rock,fall,up,sound;
    public float upspeed,time1,highy,lowy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rockUp();
    }
    public void rockUp(){
        if(Rock.transform.position.y<=lowy||Rock.transform.position.y<highy){    
            Rock.transform.Translate(0,upspeed*Time.deltaTime,0);
        }
        if(Rock.transform.position.y>=highy){
            sound.SetActive(false);
            Invoke("off",time1);
        }
    }
    public void off(){
        
        fall.SetActive(true);
        up.SetActive(false);
    }
}
