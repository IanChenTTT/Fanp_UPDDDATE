using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class passfade_closeblack : MonoBehaviour
{ 
    public GameObject CG;
    public float x;
    private float alpha=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cg();
    }
    public void cg(){
        alpha=alpha-x*Time.deltaTime;
        if(alpha>0){
            CG.GetComponent<Image>().color = new Color(CG.GetComponent<Image>().color.r,CG.GetComponent<Image>().color.g,CG.GetComponent<Image>().color.b,alpha);
        }   
        else if(alpha<=0){
            CG.SetActive(false);
        }  
    }
}
