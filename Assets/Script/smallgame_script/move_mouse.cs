using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_mouse : MonoBehaviour
{
    // private Vector2 mousePos;
    // private float limitY;
    // Start is called before the first frame update
    [SerializeField] float movespeed=9.0f;
    public float speedup;
    public GameObject player;
     public Vector3 minPosition; // 矩形区域的最小位置
    public Vector3 maxPosition; // 矩形区域的最大位置
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cursor_move();
        move();
        
        turn();
        range();
    }

    public void cursor_move()
    {
        // mousePos = new Vector2(0,Input.mousePosition.y);
        // limitY = Mathf.Clamp(mousePos.y , 0.25f , Screen.height - 0.25f);
        // mousePos = new Vector2(0,limitY);
        // transform.position = mousePos;
        Vector3 mouse = Input.mousePosition;
        Vector3 obj = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = mouse - obj;
        direction.z=0f;
        direction = direction.normalized;
        transform.up=direction;
    }
    public void move()
    {
        if(Input.GetMouseButton(0))
        {
            transform.Translate(Vector3.up * (movespeed+speedup) * Time.deltaTime);
        }
        else
        {
             transform.Translate(Vector3.up * movespeed * Time.deltaTime);
        }
    }

    public void turn()
    {
        //滑鼠位置
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
        //print(mousePos.x);
        player = GameObject.Find("GameObject_bigbird");
        if(mousePos.x<=300)
        {
            // this.gameObject.transform.localScale.x=-1;
            player.transform.localScale = new Vector3(-1, 1, 1);
        }
        if(mousePos.x>300)
        {
            // this.gameObject.transform.localScale.x=-1;
            player.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void range()
    {
         // 获取物体当前的位置
        Vector3 position = transform.position;

        // 如果物体超出了范围，将其位置设置为范围的边界
        if (position.x < minPosition.x) position.x = minPosition.x;
        if (position.x > maxPosition.x) position.x = maxPosition.x;
        if (position.y < minPosition.y) position.y = minPosition.y;
        if (position.y > maxPosition.y) position.y = maxPosition.y;
        if (position.z < minPosition.z) position.z = minPosition.z;
        if (position.z > maxPosition.z) position.z = maxPosition.z;

        // 更新物体的位置
        transform.position = position;
    }
}
