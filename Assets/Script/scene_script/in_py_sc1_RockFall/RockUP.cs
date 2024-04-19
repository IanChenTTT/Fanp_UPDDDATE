using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockUP : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Rock,fall,up,sound;
    public float upspeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rockUp();
    }
    public void rockUp(){
        if(Rock.transform.position.y<=7.5||Rock.transform.position.y<17){    
            Rock.transform.Translate(0,upspeed*Time.deltaTime,0);
        }
        if(Rock.transform.position.y>=17){
            sound.SetActive(false);
            fall.SetActive(true);
            up.SetActive(false);
        }
    }
}
