using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPivot : MonoBehaviour
{
    public Camera m_camera;

    public Transform gun_holder;
    public Transform fire_point;

    public GameObject bullet;

    public GameObject fireeffect;

    private void Update()
    {
        RotateGun();
        PlayerInput();
    }

    void RotateGun() 
    {
        Vector2 distanceVector = (Vector2)m_camera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)gun_holder.position;
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        Vector2 scale=transform.localScale;
        if(distanceVector.x<0)
        {
            scale.y=-1;
        }else if(distanceVector.x>0)
        {
            scale.y=1;
        }
        transform.localScale = scale;
        

        /*
        Vector2 scale=transform.localScale;
        if(Input.GetKeyDown(KeyCode.A))
        {
            scale.y=-1;
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            scale.y=1;
        }
        transform.localScale = scale;
        */
    }

    void PlayerInput() 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            Instantiate(bullet, fire_point.position, transform.rotation);
            Instantiate(fireeffect, fire_point.position, transform.rotation);
        }
    }

}
