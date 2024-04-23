using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class end_change_scece : MonoBehaviour
{
    public bool turnOn=false;
    public GameObject CG;
    public float x;
    private float alpha=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(turnOn==true){
            cg();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        turnOn=true;
        if(other.tag=="Player"){
            Invoke("changescene",1.0f);
        }
    }
    public void changescene(){
        SceneManager.LoadScene("take_off_ani");
    }
    public void cg(){
        alpha=alpha+x*Time.deltaTime;
        if(alpha<1){
            CG.GetComponent<Image>().color = new Color(CG.GetComponent<Image>().color.r,CG.GetComponent<Image>().color.g,CG.GetComponent<Image>().color.b,alpha);
        }   
    }
}
