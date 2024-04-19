using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityAffect : MonoBehaviour
{
    public GameObject objectToFloat;
    public float AffectGravity = -1f;
    public float OverGravity = 1f;

   
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projecter")
        {
            Invoke("changeGravity", 0.05f);
            Invoke("overGravity", 2f);
        }
        
    }
    void changeGravity() 
    {
            Rigidbody2D rb = objectToFloat.GetComponent<Rigidbody2D>();

            rb.gravityScale = AffectGravity;
    }
    void overGravity()
    {
            Rigidbody2D rb = objectToFloat.GetComponent<Rigidbody2D>();

            rb.gravityScale = OverGravity;
    }
}
