using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowNew : MonoBehaviour
{
    public float speed = 20;
    [SerializeField]
    private Rigidbody2D bulletRb;
    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D hitInfo){
        //Any help collider will not collide with bullet
        if(!hitInfo.name.Contains("Collider"))
        {
            Destroy(this.gameObject);
        }
    }
    public void InitializeVelocity(Vector2 direction){
        this.bulletRb.velocity =  direction * speed;
    }
}
