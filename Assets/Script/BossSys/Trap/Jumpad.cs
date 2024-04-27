using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Jumpad : MonoBehaviour
{
    public float forceMagnitude; // Adjust this value to control the force strength
    private bool IsTouch = false;
    private bool IsPress = false;
    Collision2D collidWith;
    private void Update(){
        // Reduce amount of Input event;
        IsPress = InputManage.Instance.OnPressSpcae();
    }
    private void FixedUpdate(){
        if(IsTouch && IsPress)
        {
            Debug.Log("Suppose to jump");
            GameObject OBJ = collidWith.gameObject;
            Debug.Log(OBJ + "1");
            Debug.Log(forceMagnitude + "1");
            Debug.Log(forceMagnitude+" "+Vector2.up + "1");
            OBJ.GetComponent<Rigidbody2D>().AddForce(Vector2.up* forceMagnitude,ForceMode2D.Impulse);
            IsTouch = false;
        }
    }
   private void OnCollisionEnter2D(Collision2D collision2D){
        if(collision2D.gameObject.CompareTag("Player") || collision2D.gameObject.CompareTag("Boss")){

            Debug.Log("Suppose to jump" + " "+ collision2D.gameObject);
            this.collidWith = collision2D;
            this.IsTouch = true;
        }
   }

}
