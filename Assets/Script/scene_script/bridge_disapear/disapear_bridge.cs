using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disapear_bridge : MonoBehaviour
{
    public GameObject bridge;
    public float x;
    private float alpha=1;
    public bool trunoff=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count();
        alpha0_or_alpha1();
    }


    void OnTriggerStay2D(Collider2D other)
    {
        trunoff=true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        trunoff=false;
    }
    public void alpha0_or_alpha1(){
        if(alpha<=0){
            bridge.SetActive(false);
        }
        else if(alpha>=1){
            bridge.SetActive(true);
        }
    }
    public void count(){
        if(trunoff==true){
            alpha=alpha-x*Time.deltaTime;
                bridge.GetComponent<SpriteRenderer>().color = new Color(bridge.GetComponent<SpriteRenderer>().color.r,bridge.GetComponent<SpriteRenderer>().color.g,bridge.GetComponent<SpriteRenderer>().color.b,alpha);
        }
        else if(trunoff==false&&alpha<=1){
            alpha=alpha+x*Time.deltaTime;
                bridge.GetComponent<SpriteRenderer>().color = new Color(bridge.GetComponent<SpriteRenderer>().color.r,bridge.GetComponent<SpriteRenderer>().color.g,bridge.GetComponent<SpriteRenderer>().color.b,alpha);
        }
    }
}
