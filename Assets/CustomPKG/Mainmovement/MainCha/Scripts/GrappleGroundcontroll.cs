using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGroundcontroll : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
            {
                rb.velocity = new Vector2(0f, 0f); 
                StartCoroutine(ExecuteFunctionForSeconds());
            }
            if(Input.GetKeyUp(KeyCode.D))
            {
                rb.velocity = new Vector2(0f, 0f); 
                StartCoroutine(ExecuteFunctionForSeconds());
            }
    }
    
    IEnumerator ExecuteFunctionForSeconds()
    {
        float elapsedTime = 0f;
        // 在时间未达到x秒之前一直执行
        while (elapsedTime < 0.2f)
        {
            // 执行您想要持续执行的函数
            YourFunction();
            // 等待一帧
            yield return null;
            // 更新已经经过的时间
            elapsedTime += Time.deltaTime;
        }
    }
    void YourFunction()
    {
        rb.velocity = new Vector2(0f, 0f); 
    }
    
}
