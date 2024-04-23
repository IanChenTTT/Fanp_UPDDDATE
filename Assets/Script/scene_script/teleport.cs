using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public float x,y;
    public GameObject palyerpoint; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Tele(){
        palyerpoint.transform.position=new Vector2(x,y);
    }
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player"){
           lagtele(); 
        }
    }
    public void lagtele(){
        Invoke("Tele",0.9f);
    }
}
