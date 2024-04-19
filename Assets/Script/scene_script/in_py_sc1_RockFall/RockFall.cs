using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFall : MonoBehaviour
{
    public GameObject Rock,fall,up;
    public float fallspeed;

    public GameObject sound;
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
        if(Rock.transform.position.y<=17&&Rock.transform.position.y>7.5||Rock.transform.position.y>17){    
            Rock.transform.Translate(0,fallspeed*Time.deltaTime,0);
        }
        if(Rock.transform.position.y<=7.5){
            sound.SetActive(true);
            fall.SetActive(false);
            up.SetActive(true);
        }
    }
}
