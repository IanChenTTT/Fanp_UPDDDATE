using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletNew : MonoBehaviour
{
    public float speed = 20;
    [SerializeField]
    private Rigidbody2D bulletRb;
    private GameObject Father;
    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D hitInfo){
        //Any help collider will not collide with bullet
        if(!hitInfo.name.Contains("Collider"))
        {
            this.gameObject.SetActive(false);
            Father.GetComponent<FirePoint>().bulletQue.EnqueOBJ(this.gameObject);
            // if(hitInfo.name.Contains("Scar"))
        }
    }
    public void SetFatherIs(GameObject obj){
        this.Father = obj; 
    }
    public void InitializeVelocity(Vector2 direction){
        bulletRb.velocity =  direction * speed;
    }
}
