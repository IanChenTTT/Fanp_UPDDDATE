using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleIOC : MonoBehaviour
{
    public MonoBehaviour scriptToControl;
    public Rigidbody2D rb;
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
        {
            scriptToControl.enabled = true;
            rb.mass=5f;
            rb.velocity = new Vector2(0f, 0f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
        {
            scriptToControl.enabled = false;
            rb.mass=80f;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
        {
            scriptToControl.enabled = true;
            rb.mass=5f;
        }
    }
    
}
