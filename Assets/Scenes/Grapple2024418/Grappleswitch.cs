using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappleswitch : MonoBehaviour
{
    public GameObject grapple;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
        {
            grapple.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
        {
            grapple.SetActive(true);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Ground"))
        {
            grapple.SetActive(false);
        }
        
    }
}
