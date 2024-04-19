using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_left_fly : MonoBehaviour
{
    public float speed;
    public int distroy_time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position -= new Vector3(speed,0,0);
        Destroy(gameObject,distroy_time);
    }
}
