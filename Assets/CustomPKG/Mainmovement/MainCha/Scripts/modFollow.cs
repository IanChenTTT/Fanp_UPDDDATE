using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modFollow : MonoBehaviour
{
    public Transform target;
    public float a=0.3f;
    void Update()
    {
        if (target != null)
        {
            // 设置 follower 的位置为目标物体的位置
            // if(Input.GetKeyDown(KeyCode.D))
            // {
            //     a=0.2f;
            // }
            // if(Input.GetKeyDown(KeyCode.A))
            // {
            //     a=0.019f;
            // }
            // Vector3 newPosition = new Vector2(target.position.x-a, target.position.y +(float)0.04);
            Vector3 newPosition = new Vector2(target.position.x, target.position.y);
            transform.position = newPosition;
        }
    }

}