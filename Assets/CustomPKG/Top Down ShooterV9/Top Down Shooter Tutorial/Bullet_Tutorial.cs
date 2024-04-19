using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet_Tutorial : MonoBehaviour
{
    public float bullet_speed = 4;
    public float bullet_duration = 4;

    Rigidbody2D bullet_rigidbody;

    private void Awake()
    {
        bullet_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Invoke("DestroyBullet", bullet_duration);
    }

    private void FixedUpdate()
    {
        bullet_rigidbody.MovePosition(transform.position + transform.right * bullet_speed * Time.fixedDeltaTime);

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke("DestroyBullet", 0.05f);
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "GravityAffect")
        {
            Destroy(this.gameObject);
        }
    }
    void DestroyBullet() 
    {
        Destroy(gameObject);
    }
}

