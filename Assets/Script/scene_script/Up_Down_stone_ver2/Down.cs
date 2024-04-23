using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down : MonoBehaviour
{
    public GameObject Rock,fall,up,sound;
    public float fallspeed,time1,highy,lowy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rockfall();
    }
    public void rockfall(){
        if(Rock.transform.position.y<=highy&&Rock.transform.position.y>lowy||Rock.transform.position.y>highy){    
            Rock.transform.Translate(0,fallspeed*Time.deltaTime,0);
        }
    if(Rock.transform.position.y<=lowy){
            sound.SetActive(true);
            Invoke("off",time1); 
        }
    }
    public void off(){
       
        fall.SetActive(false);
        up.SetActive(true);
    }
}
