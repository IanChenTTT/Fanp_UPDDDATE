using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class men_annim : MonoBehaviour
{
        private Animator ani ;
        public bool swing_oction;
    // Start is called before the first frame update
    void Start()
    {
            ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetBool("IsRunning",false);
        ani.SetBool("IsDying",false);
        Keypress_Anim();
    }

    public void Keypress_Anim()
    {
        if (Input.GetKey (KeyCode.A)){
            ani.SetBool("IsRunning",true);
        }
        
        if (Input.GetKey (KeyCode.D))
        {
            ani.SetBool("IsRunning",true);
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            ani.SetBool("IsJumping",true);
        }
    // if (Input.GetKeyDown(KeyCode.X)) 
    //     {
    //         ani.SetBool("IsDying",true);
    //     }
    if(swing_oction==true)
    {
        if(Input.GetMouseButton(0))
        {
            ani.SetBool("IsSwing",true);
            if(ani.GetBool ("IsSwing") == true)
            {
                ani.SetBool("IsJumping",false);
            }
        }
        else
        {
             ani.SetBool("IsSwing",false);
        }
    }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        //ani.SetBool("IsJumping",false);
        //TAG要設ground才會停止跳躍動畫
        if(other.CompareTag("Ground"))
        {
            ani.SetBool("IsJumping",false);
        }   
                    
        /*if(other.CompareTag("swing"))
        {
            ani.SetBool("IsJumping",false);
            ani.SetBool("IsSwing",true);
        }*/
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Ground"))
        {
            ani.SetBool("IsJumping",false);
        }   
    }
    private void OnTriggerExit2D(Collider2D other) {
      if(other.CompareTag("Ground"))
        {
            ani.SetBool("IsJumping",true);
        }   
    }
}
