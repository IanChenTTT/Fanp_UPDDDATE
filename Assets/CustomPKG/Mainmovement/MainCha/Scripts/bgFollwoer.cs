using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgFollwoer : MonoBehaviour
{
    public Transform target;  // 要跟随的目标物体
    public float yOffset = 2f;  // 在Y轴上的偏移值
    public float maxY = 10f;    // Y轴的最大值
    public float minY = 2f;     // Y轴的最小值

    void Update()
    {
        // 检查是否有指定的目标物体
        if (target != null)
        {
            // 设置 follower 的位置为目标物体的位置
            Vector3 newPosition = new Vector2(target.position.x, Mathf.Clamp(target.position.y + yOffset, minY, maxY));
            transform.position = newPosition;
        }
    }
}
