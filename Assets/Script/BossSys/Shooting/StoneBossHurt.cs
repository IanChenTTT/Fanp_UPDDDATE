using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBossHurt : MonoBehaviour
{
     private Animator scarAnimator;
     void Start(){
        scarAnimator = this.GetComponent<Animator>();
     }
     void OnTriggerEnter2D(Collider2D hitInfo){
        Debug.Log("Hitting:"+hitInfo.name);
        //Any help collider will not collide with bullet
        if(hitInfo.CompareTag("Bullet"))
        {
            Debug.Log("It hurrrrt");
            scarAnimator.SetBool("isHurt", true);
            Stone_Boss.Health --;
            Debug.Log(Stone_Boss.Health);
        }
    }
}
