using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class pass_fade_end : MonoBehaviour
{    public GameObject CG;
    public float x;
    private float alpha=0;
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
    
        alpha=alpha+x*Time.deltaTime;
        if(alpha<1){
            CG.GetComponent<Image>().color = new Color(CG.GetComponent<Image>().color.r,CG.GetComponent<Image>().color.g,CG.GetComponent<Image>().color.b,alpha);
        }     
    }
}
