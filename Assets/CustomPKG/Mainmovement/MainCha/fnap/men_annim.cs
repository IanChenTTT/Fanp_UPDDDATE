using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class men_annim : MonoBehaviour
{
        private Animator ani ;
        public GameObject hook_joint2d;
        // public bool swing_oction;
    void Start()
    {
            ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetBool("IsRunning",false);
        Keypress_Anim();
    }

    public void Keypress_Anim()
    {
        if (Input.GetButton ("Horizontal")||Input.GetButton ("Vertical"))
        {
            ani.SetBool("IsRunning",true);
        }
        else
        {
            ani.SetBool("IsRunning",false);
        }
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            ani.SetBool("IsJumping",true);
        }

        if(hook_joint2d.GetComponent<SpringJoint2D>().enabled == true)
        {
            ani.SetBool("IsSwing",true);
            ani.Play("swing");
            if(ani.GetBool("IsSwing")==true)
            {
                ani.SetBool("IsJumping",false);
            }
        }
        else
        {
            ani.SetBool("IsSwing",false);
        }
        /*if(swing_oction==true)
        {
            if(Input.GetMouseButton(0))
            {
                ani.SetBool("IsSwing",true);
                ani.Play("swing");
                if(ani.GetBool("IsSwing")==true)
                {
                    ani.SetBool("IsJumping",false);
                    ani.SetBool("IsRunning",false);
                }
            }
            else
            {
                ani.SetBool("IsSwing",false);
            }
        }*/


    }

    private void OnTriggerEnter2D(Collider2D other) {
        //ani.SetBool("IsJumping",false);
        //TAG要設ground才會停止跳躍動畫
        if(other.CompareTag("Ground"))
        {
            ani.SetBool("IsJumping",false);
            ani.Play("idl");
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
