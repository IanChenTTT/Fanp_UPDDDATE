using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class jumpPad : MonoBehaviour
{
     public float forceMagnitude = 100f; // Adjust this value to control the force strength
    public float smoothTime = 0.05f; // Adjust this value to control the smoothness of force application

    private Vector2 currentVelocity = Vector2.up;

    private float bounce = 300f;
    public float dampingRatio = 0.5f;
    public float offset = 5;
    public bool spaceDown =  false;

    
    void FixedUpdate()
    {   // }
        spaceDown = Input.GetKey ( KeyCode.Space );
     
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("padAffect") && spaceDown)
        {            
          
                 Vector2 forceDirection = Vector2.up;
            
             // Smoothly apply force using Vector2.SmoothDamp
                Vector2 smoothForce = Vector2.SmoothDamp(other.GetComponent<Rigidbody2D>().velocity, forceDirection * forceMagnitude, ref currentVelocity,Time.fixedDeltaTime);
                Debug.Log(smoothForce);

             // Apply the force to the Rigidbody2D
           
                other.GetComponent<Rigidbody2D>().AddForce(smoothForce, ForceMode2D.Impulse);
            
        }   
        
    }
}