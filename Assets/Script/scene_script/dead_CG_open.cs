using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class dead_CG_open : MonoBehaviour
{
    public GameObject CG;
    public float x;
    public float alpha=0;
    public bool alphaopen=false;
    // Start is called before the first frame update
    void Start()
    {
        open_CG();
    }

    // Update is called once per frame
    void Update()
    {
        cg();
    }
    public void cg(){
        if(alphaopen==true){
        alpha=alpha+x*Time.deltaTime;
        if(alpha<1){
            CG.GetComponent<Image>().color = new Color(CG.GetComponent<Image>().color.r,CG.GetComponent<Image>().color.g,CG.GetComponent<Image>().color.b,alpha);
        }
        else if(alpha>=1){
            alphaopen=false;
        }
    }     
    }
    public void open_CG(){
        Invoke("Bool",0.8f);
    }
    public void Bool(){
        alphaopen=true;
    }
}
