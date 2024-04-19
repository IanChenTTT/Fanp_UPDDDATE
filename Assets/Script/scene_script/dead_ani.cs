using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dead_ani : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel,player_ani;
    //private Animator ani ;
    void Start()
    {
        //ani = GetComponent<Animator>();
        ani_over();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ani_over(){
        Invoke("Setopen",0.8f);
    }
    public void Setopen(){
        panel.SetActive(true);
        player_ani.SetActive(false);
    }
}
