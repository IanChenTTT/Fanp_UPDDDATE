using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UltimateCC;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class attachrope : MonoBehaviour
{
    private FixedJoint2D fixedJoint;

    private Vector2 otherVelcity;
    private float angularVelocity;
    private Rigidbody2D otherRigidbody;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            otherRigidbody = collision.collider.GetComponent<Rigidbody2D>();
            if (otherRigidbody != null&& fixedJoint == null)
            {
            // 创建FixedJoint2D
                fixedJoint = gameObject.AddComponent<FixedJoint2D>();
                fixedJoint.connectedBody = otherRigidbody;

            // 可以根据需要调整其他关节参数
                fixedJoint.enableCollision = true;
                fixedJoint.breakForce = Mathf.Infinity; // 如果需要，设置一个打破力
                collision.gameObject.GetComponent<Rigidbody2D>().mass=1.0f;
            }
        }
    }
    
    void FixedUpdate()
    {
        if (fixedJoint != null && otherRigidbody != null)
        {
            //每幀儲存慣性
            otherVelcity= otherRigidbody.velocity;
            angularVelocity=otherRigidbody.angularVelocity;

            Vector2 Velocityx = new Vector2(6.0f, 0f);
            Vector2 Velocityy = new Vector2(1.0f,60.0f);

            if(Input.GetKey(KeyCode.F))
            {
                //UnityEngine.Debug.Log("!!!");
                Destroy(GetComponent<FixedJoint2D>());
                otherRigidbody.AddForce(Velocityy,ForceMode2D.Impulse);
            }
            if(Input.GetKey(KeyCode.D))
            {
                for(int i=0;i<2;i++)
                {
                    GetComponent<Rigidbody2D>().AddForce(Velocityx, ForceMode2D.Impulse);
                }
            }
            if(Input.GetKey(KeyCode.A))
            {
                for(int i=0;i<2;i++)
                {
                    GetComponent<Rigidbody2D>().AddForce(-Velocityx, ForceMode2D.Impulse);
                }
            }
        }
    }
    
}
