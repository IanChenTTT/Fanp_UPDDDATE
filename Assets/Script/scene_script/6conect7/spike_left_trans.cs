using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike_left_trans : MonoBehaviour
{
    public float speed,false_time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position -= new Vector3(speed * Time.deltaTime,0,0);
        Invoke("activefalse",false_time);
    }

    public void activefalse()
    {
        this.gameObject.SetActive(false);
    }
}
