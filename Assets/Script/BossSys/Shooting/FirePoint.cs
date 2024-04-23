using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class FirePoint : MonoBehaviour
{
    bool isFaceRight = true;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private GameObject bullet;
    public GameOBJ_QUE bulletQue;
    const int QUE_SIZE = 25;
    private void Start(){
        bulletQue = new GameOBJ_QUE(QUE_SIZE);
        int i;
        for(i = 0 ; i < QUE_SIZE ; i++){
           GameObject obj = Instantiate(bullet, bullet.transform.position, Quaternion.identity); 

           //IMPORTANT PLS LOOK
           obj.SetActive(false);
           bulletQue.EnqueOBJ(obj);
        }
        Debug.Log(bulletQue.CAPACITY);
    }
    void Update()
    {
        // SRC: https://www.youtube.com/watch?v=FgI8cgYAewM
        Vector3 mousePos = Input.mousePosition;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 gunPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x -= gunPos.x;
        mousePos.y -= gunPos.y;
        float gunAngle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg; // Unity formula calculate Angle
        if(mousePosWorld.x < transform.position.x){
            transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -gunAngle));
        }
        else{
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, gunAngle));
        }
       
        if((mousePosWorld.x < playerTransform.position.x)  && isFaceRight)
            Flip();
        else if( (mousePosWorld.x > playerTransform.position.x)&& !isFaceRight)
            Flip();
        if(Input.GetButtonDown("Fire1")){
            Shoot();        
        }
    }
    void Shoot(){
        // Mouse to world poision By https://discussions.unity.com/t/shooting-in-direction-of-mouse-cursor-2d/90904/5
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        
        Vector2 direction = (Vector2)(worldMousePos - transform.position);
        direction.Normalize();

        // Creates the bullet locally NOT QUEUE VER1
        // GameObject bulletRef = (GameObject)Instantiate(
        //                         bullet,
        //                         transform.position + (Vector3)(direction * 0.5f),
        //                         Quaternion.identity);
        GameObject bulletRef = bulletQue.DequeOBJ();
        if(bulletRef != null){
            bulletRef.SetActive(true);
            bulletRef.transform.SetPositionAndRotation(transform.position + (Vector3)(direction * 0.5f), Quaternion.Euler(0,0,0));
            // Adds velocity to the bullet
            bulletRef.GetComponent<BulletNew>().InitializeVelocity(direction);
            bulletRef.GetComponent<BulletNew>().SetFatherIs(this.gameObject); //COMPOSITION GET PARENT

            Debug.Log(bulletQue.CAPACITY);
        }
        else{
            Debug.LogWarning("Que is not return obj"+bulletQue.CAPACITY);
        }
    }
    void Flip()
    {
      isFaceRight = !isFaceRight;
      playerTransform.Rotate(0f, 180f,0f);
      transform.Rotate(0f, 180f, 0f);
    }
}
