using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunGameOpen : MonoBehaviour
{
    public GameObject gun,teleToGun,teleTorope;
    static public bool getgun=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Getgun();
    }
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        gun.SetActive(false);
        getgun=true;
    }
    public void Getgun(){
        if(getgun==true){
            teleToGun.SetActive(true);
            teleTorope.SetActive(false);
        }
        if(getgun==false){
            teleToGun.SetActive(false);
            teleTorope.SetActive(true); 
        }
    }
}
