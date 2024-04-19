using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFocus : MonoBehaviour
{
    public Transform target;  // 角色的 Transform 组件
    public Vector3 offset;    // 相机与角色的偏移量

    void LateUpdate()
    {
        // 检查是否有指定的角色 Transform
        if (target != null)
        {
            // 获取角色的位置，并加上偏移量
            Vector3 targetPosition = target.position + offset;

            // 设置相机的位置为角色的位置
            transform.position = targetPosition;

            // 让相机始终看向角色
            transform.LookAt(target.position);
        }
    }
}
