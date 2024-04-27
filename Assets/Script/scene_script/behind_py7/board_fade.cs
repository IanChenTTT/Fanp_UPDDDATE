using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board_fade : MonoBehaviour
{
    public GameObject board_black;
    public float x;
    private float alpha=1;
    public GameObject item;
    public bool tri=false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(tri==true){
            fade();
        }
  
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        tri=true;
        item.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
    }
    public void fade(){
            alpha=alpha-x*Time.deltaTime;
        if(alpha>0){
            board_black.GetComponent<SpriteRenderer>().color = new Color(board_black.GetComponent<SpriteRenderer>().color.r,board_black.GetComponent<SpriteRenderer>().color.g,board_black.GetComponent<SpriteRenderer>().color.b,alpha);
        }
        else if(alpha<=0){
            board_black.SetActive(false);
            
        }
             
    }
}
