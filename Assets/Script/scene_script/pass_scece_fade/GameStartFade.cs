using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartFade : MonoBehaviour
{
    public GameObject fade;
    public float x;
    public float alpha=1;
    public bool alphaopen=false;
    public GameObject StartFade;
    // Start is called before the first frame update
    void Start()
    {
        Bool();
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
            fade.GetComponent<Image>().color = new Color(fade.GetComponent<Image>().color.r,fade.GetComponent<Image>().color.g,fade.GetComponent<Image>().color.b,alpha);
        }
        else if(alpha>=1){
            alphaopen=false;
            StartFade.SetActive(false);
        }
    }     
    }
        public void Bool(){
        alphaopen=true;
    }
}
